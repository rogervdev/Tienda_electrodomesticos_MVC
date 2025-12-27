using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using Tienda_electrodomesticos_MVC.Models.ViewModels;
using Tienda_electrodomesticos_MVC.Models;

namespace Tienda_electrodomesticos_MVC.Controllers
{
    public class CuentaController : Controller
    {
        private readonly HttpClient _http;

        public CuentaController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("api");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var response = await _http.PostAsJsonAsync("usuario/login", model);

                if (response.IsSuccessStatusCode)
                {
                    var usuario = await response.Content.ReadFromJsonAsync<Usuario>();

                    // Guardar en sesión
                    HttpContext.Session.SetString("UsuarioId", usuario!.Id.ToString());
                    HttpContext.Session.SetString("UsuarioEmail", usuario.Email);
                    HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
                    HttpContext.Session.SetString("UsuarioRol", usuario.Rol);

                    TempData["Success"] = "Bienvenido " + usuario.Email;

                    // Redirigir según rol
                    if (usuario.Rol.ToUpper() == "ROLE_ADMIN")
                        return RedirectToAction("Index", "Admin"); // panel admin
                    else
                        return RedirectToAction("Index", "Home");  // usuario normal
                }
                else
                {
                    var mensaje = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = mensaje;
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ocurrió un error al iniciar sesión: " + ex.Message;
                return View(model);
            }
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Success"] = "Has cerrado sesión correctamente.";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // 🔹 Mapeo ViewModel → DTO API
            var dto = new
            {
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Email = model.Email,
                Password = model.Password
            };

            var response = await _http.PostAsJsonAsync("usuario/registrar", dto);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Usuario registrado correctamente";
                return RedirectToAction("Login");
            }

            TempData["Error"] = "Error al registrar usuario";
            return View(model);
        }



    }
}
