using Microsoft.AspNetCore.Mvc;
using Tienda_electrodomesticos_MVC.Services;

public class UsuariosController : Controller
{
    private readonly UsuarioApiService _usuarioService;

    public UsuariosController(UsuarioApiService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public async Task<IActionResult> Index()
    {
        var usuarios = await _usuarioService.ListarUsuariosPorRol("ROLE_USER");
        return View(usuarios);
    }

    public async Task<IActionResult> Detalles(string email)
    {
        var usuario = await _usuarioService.ObtenerPorEmail(email);
        if (usuario == null) return NotFound();

        return View(usuario);
    }
}
