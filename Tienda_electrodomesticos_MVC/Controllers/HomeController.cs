using Microsoft.AspNetCore.Mvc;
using Tienda_electrodomesticos_MVC.Models;
using Tienda_electrodomesticos_MVC.Models.ViewModels;
using Tienda_electrodomesticos_MVC.Services;

namespace Tienda_electrodomesticos_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _http;
        private readonly CarritoApiService _carritoService;

        public HomeController(IHttpClientFactory factory, CarritoApiService carritoService)
        {
            _http = factory.CreateClient("api");
            _carritoService = carritoService;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _http.GetFromJsonAsync<List<Categoria>>("categoria");
            var productos = await _http.GetFromJsonAsync<List<Producto>>("producto/activos");

            // ?? IMPORTANTE PARA EL LAYOUT
            ViewBag.Categorias = categorias ?? new List<Categoria>();

            var vm = new HomeViewModel
            {
                Categorias = categorias ?? new(),
                Productos = productos ?? new()
            };

            // Sesión
            if (HttpContext.Session.GetString("UsuarioId") != null)
            {
                int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId")!);

                ViewBag.Usuario = new
                {
                    Email = HttpContext.Session.GetString("UsuarioEmail"),
                    Nombre = HttpContext.Session.GetString("UsuarioNombre"),
                    Rol = HttpContext.Session.GetString("UsuarioRol")
                };

                try
                {
                    ViewBag.CountCart = await _carritoService.ContarCarrito(usuarioId);
                }
                catch
                {
                    ViewBag.CountCart = 0;
                }
            }

            return View(vm);
        }

    }
}
