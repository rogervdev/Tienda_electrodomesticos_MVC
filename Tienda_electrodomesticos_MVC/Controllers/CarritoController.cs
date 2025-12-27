using Microsoft.AspNetCore.Mvc;
using Tienda_electrodomesticos_MVC.Models;
using Tienda_electrodomesticos_MVC.Services;

namespace Tienda_electrodomesticos_MVC.Controllers
{
    public class CarritoController : Controller
    {
        private readonly CarritoApiService _carritoService;
        private readonly ProductoApiService _productoService;
        public CarritoController(CarritoApiService carritoService, ProductoApiService productoService)
        {
            _carritoService = carritoService;
            _productoService = productoService;
        }

        // Mostrar carrito
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UsuarioId") == null)
                return RedirectToAction("Login", "Cuenta");

            int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId")!);

            var carrito = await _carritoService.ObtenerCarritoPorUsuario(usuarioId);
            var productos = await _productoService.GetAllProductos();

            foreach (var item in carrito)
            {
                item.Producto = productos.FirstOrDefault(p => p.Id == item.ProductoId);
            }

            return View(carrito);
        }

        // Agregar al carrito
        [HttpPost]
        public async Task<IActionResult> Agregar(int productoId)
        {
            if (HttpContext.Session.GetString("UsuarioId") == null)
            {
                TempData["ErrorMsg"] = "Debes iniciar sesión para agregar productos al carrito.";
                return RedirectToAction("Login", "Cuenta");
            }

            int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId")!);

            await _carritoService.AgregarAlCarrito(usuarioId, productoId);

            // Actualizar contador en TempData (opcional)
            TempData["SuccessMsg"] = "Producto agregado al carrito.";

            return RedirectToAction("Detalle", "Productos", new { id = productoId });
        }

        // Contador JSON (para actualizar layout)
        public async Task<IActionResult> Contador()
        {
            if (HttpContext.Session.GetString("UsuarioId") == null)
                return Json(0);

            int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId")!);
            var count = await _carritoService.ContarCarrito(usuarioId);
            return Json(count);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int productoId)
        {
            int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId")!);
            bool eliminado = await _carritoService.EliminarProducto(usuarioId, productoId); // llama al service MVC
            TempData["SuccessMsg"] = eliminado ? "Producto eliminado del carrito." : "No se pudo eliminar el producto.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarCantidad(int productoId, string accion)
        {
            int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId")!);

            var carrito = await _carritoService.ObtenerCarritoPorUsuario(usuarioId);
            var item = carrito.FirstOrDefault(c => c.ProductoId == productoId);
            if (item == null) return RedirectToAction("Index");

            if (accion == "mas")
                await _carritoService.AumentarCantidad(usuarioId, productoId);
            else if (accion == "menos")
                await _carritoService.DisminuirCantidad(usuarioId, productoId);

            return RedirectToAction("Index");
        }

    }
}
