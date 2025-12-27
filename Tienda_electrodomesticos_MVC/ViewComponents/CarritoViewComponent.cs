using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tienda_electrodomesticos_MVC.Services;

namespace Tienda_electrodomesticos_MVC.ViewComponents
{
    public class CarritoViewComponent : ViewComponent
    {
        private readonly CarritoApiService _carritoService;

        public CarritoViewComponent(CarritoApiService carritoService)
        {
            _carritoService = carritoService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int countCart = 0;

            if (HttpContext.Session.GetString("UsuarioId") != null)
            {
                int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId")!);
                countCart = await _carritoService.ContarCarrito(usuarioId);
            }

            return View(countCart); // enviamos solo el número al view
        }
    }
}
