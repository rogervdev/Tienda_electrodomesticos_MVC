using Microsoft.AspNetCore.Mvc;
using Tienda_electrodomesticos_MVC.Services;
using Tienda_electrodomesticos_MVC.Models;

public class CategoriasController : Controller
{
    private readonly CategoriaApiService _categoriaService;

    public CategoriasController(CategoriaApiService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    public async Task<IActionResult> Index()
    {
        var categorias = await _categoriaService.GetAllCategorias();
        return View(categorias);
    }

    public async Task<IActionResult> Detalles(int id)
    {
        var categoria = await _categoriaService.GetCategoriaById(id);
        if (categoria == null) return NotFound();
        return View(categoria);
    }
}
