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
            // Traer categorías y productos
            var categorias = await _http.GetFromJsonAsync<List<Categoria>>("categoria");
            var productos = await _http.GetFromJsonAsync<List<Producto>>("producto/activos");

            // Guardar en ViewBag para el layout
            ViewBag.Categorias = categorias ?? new List<Categoria>();

            // ViewModel para la vista
            var vm = new HomeViewModel
            {
                Categorias = categorias ?? new(),
                Productos = productos ?? new()
            };

            // Sesión: usuario logueado
            if (HttpContext.Session.GetString("UsuarioId") != null)
            {
                int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId")!);

                // Creamos un objeto Usuario para usar en _Layout
                ViewBag.Usuario = new Usuario
                {
                    Id = usuarioId,
                    Nombre = HttpContext.Session.GetString("UsuarioNombre")!,
                    Email = HttpContext.Session.GetString("UsuarioEmail")!,
                    Rol = HttpContext.Session.GetString("UsuarioRol")!
                };

                try
                {
                    // Contar productos en carrito
                    ViewBag.CountCart = await _carritoService.ContarCarrito(usuarioId);
                }
                catch
                {
                    ViewBag.CountCart = 0;
                }
            }
            else
            {
                ViewBag.Usuario = null;
                ViewBag.CountCart = 0;
            }

            return View(vm);
        }
    }
}
