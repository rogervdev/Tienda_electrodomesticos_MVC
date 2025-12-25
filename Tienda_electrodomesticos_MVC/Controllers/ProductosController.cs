using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using Tienda_electrodomesticos_MVC.Models;
using Tienda_electrodomesticos_MVC.Models.ViewModels;

namespace Tienda_electrodomesticos_MVC.Controllers
{
    public class ProductosController : Controller
    {
        private readonly HttpClient _http;

        public ProductosController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("api"); // Aquí usas el cliente configurado con tu URL base
        }

        public async Task<IActionResult> Index(string categoria, string filtro)
        {
            // Traer categorías desde la API
            var categorias = await _http.GetFromJsonAsync<List<Categoria>>("categoria");
            ViewBag.Categorias = categorias ?? new List<Categoria>();

            // Traer productos según filtro o categoría
            List<Producto> productos;
            if (!string.IsNullOrEmpty(categoria))
                productos = await _http.GetFromJsonAsync<List<Producto>>($"producto/activos?categoria={categoria}")
                            ?? new List<Producto>();
            else if (!string.IsNullOrEmpty(filtro))
                productos = await _http.GetFromJsonAsync<List<Producto>>($"producto/buscar?query={filtro}")
                            ?? new List<Producto>();
            else
                productos = await _http.GetFromJsonAsync<List<Producto>>("producto/activos")
                            ?? new List<Producto>();

            var vm = new ProductosViewModel
            {
                Productos = productos,
                CategoriaSeleccionada = string.IsNullOrEmpty(categoria) ? "Todos los productos" : categoria
            };

            return View(vm);
        }







        public async Task<IActionResult> Detalle(int id)
        {
            var producto = await _http.GetFromJsonAsync<Producto>($"producto/{id}");
            if (producto == null) return NotFound();

            return View(producto);
        }
    }
}
