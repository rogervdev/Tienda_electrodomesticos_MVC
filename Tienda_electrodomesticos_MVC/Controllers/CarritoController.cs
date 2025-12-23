using Microsoft.AspNetCore.Mvc;
using Tienda_electrodomesticos_MVC.Services;

public class CarritoController : Controller
{
    private readonly CarritoApiService _carritoService;

    public CarritoController(CarritoApiService carritoService)
    {
        _carritoService = carritoService;
    }

    public async Task<IActionResult> Index(int usuarioId)
    {
        var carrito = await _carritoService.ObtenerCarritoPorUsuario(usuarioId);
        return View(carrito);
    }

    [HttpPost]
    public async Task<IActionResult> Agregar(int usuarioId, int productoId)
    {
        await _carritoService.AgregarAlCarrito(usuarioId, productoId);
        return RedirectToAction("Index", new { usuarioId });
    }

    public async Task<IActionResult> Contador(int usuarioId)
    {
        var cantidad = await _carritoService.ContarCarrito(usuarioId);
        return Json(cantidad);
    }
}
