using Microsoft.AspNetCore.Mvc;
using Tienda_electrodomesticos_MVC.Services;

public class ProductosController : Controller
{
    private readonly ProductoApiService _productoService;

    public ProductosController(ProductoApiService productoService)
    {
        _productoService = productoService;
    }

    public async Task<IActionResult> Index()
    {
        var productos = await _productoService.GetAllProductos();
        return View(productos);
    }

    public async Task<IActionResult> Detalle(int id)
    {
        var producto = await _productoService.GetProductoById(id);
        if (producto == null) return NotFound();
        return View(producto);
    }
}
