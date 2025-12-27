using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Tienda_electrodomesticos_MVC.Models;
using Tienda_electrodomesticos_MVC.Models.ViewModels;
using Tienda_electrodomesticos_MVC.Services;

namespace Tienda_electrodomesticos_MVC.Controllers
{
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private readonly UsuarioApiService _usuarioService;
        private readonly ProductoApiService _productoService;
        private readonly CategoriaApiService _categoriaService;

        public AdminController(
            UsuarioApiService usuarioService,
            ProductoApiService productoService,
            CategoriaApiService categoriaService)
        {
            _usuarioService = usuarioService;
            _productoService = productoService;
            _categoriaService = categoriaService;
        }

        // -------------------- DASHBOARD --------------------
        public IActionResult Index()
        {
            return View();
        }

        // -------------------- USUARIOS --------------------
        public async Task<IActionResult> Usuarios()
        {
            var usuarios = await _usuarioService.ListarUsuariosPorRol("ROLE_USER");
            return View(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarEstadoUsuario(int id, bool estado)
        {
            await _usuarioService.ActualizarEstadoCuenta(id, estado);
            TempData["successMsg"] = "La cuenta ha sido actualizada";
            return RedirectToAction("Usuarios");
        }

        // -------------------- CATEGORÍAS --------------------
        // Listado de categorías
        public async Task<IActionResult> Categorias()
        {
            var categorias = await _categoriaService.GetAllCategoriasTodas();
            return View(categorias);
        }


        // Vista de edición
        [HttpGet]
        public async Task<IActionResult> EditarCategoria(int id)
        {
            var categoria = await _categoriaService.GetCategoriaById(id);
            if (categoria == null)
            {
                TempData["errorMsg"] = "Categoría no encontrada";
                return RedirectToAction("Categorias");
            }

            return View(categoria);
        }

        // Guardar nueva categoría
        [HttpPost]
        public async Task<IActionResult> GuardarCategoria(CategoriaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["errorMsg"] = "Datos inválidos";
                return RedirectToAction("Categorias");
            }

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(model.Nombre ?? ""), "Nombre");
            formData.Add(new StringContent(model.IsActive.ToString().ToLower()), "IsActive");

            if (model.File != null && model.File.Length > 0)
            {
                var stream = new StreamContent(model.File.OpenReadStream());
                stream.Headers.ContentType = new MediaTypeHeaderValue(model.File.ContentType);
                formData.Add(stream, "Imagen", model.File.FileName);
            }

            try
            {
                await _categoriaService.GuardarCategoria(formData);
                TempData["successMsg"] = "Categoría guardada correctamente";
            }
            catch
            {
                TempData["errorMsg"] = "Error al guardar la categoría";
            }

            return RedirectToAction("Categorias");
        }

        // Actualizar categoría existente
        [HttpPost]
        public async Task<IActionResult> ActualizarCategoria(CategoriaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["errorMsg"] = "Datos inválidos";
                return RedirectToAction("EditarCategoria", new { id = model.Id });
            }

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(model.Id.ToString()), "Id");
            formData.Add(new StringContent(model.Nombre ?? ""), "Nombre");
            formData.Add(new StringContent(model.IsActive.ToString().ToLower()), "IsActive");

            if (model.File != null && model.File.Length > 0)
            {
                var stream = new StreamContent(model.File.OpenReadStream());
                stream.Headers.ContentType = new MediaTypeHeaderValue(model.File.ContentType);
                formData.Add(stream, "Imagen", model.File.FileName);
            }

            try
            {
                await _categoriaService.ActualizarCategoria(model.Id, formData);
                TempData["successMsg"] = "Categoría actualizada correctamente";
            }
            catch
            {
                TempData["errorMsg"] = "Error al actualizar la categoría";
            }

            return RedirectToAction("EditarCategoria", new { id = model.Id });
        }

        // Eliminar categoría
        [HttpPost]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            await _categoriaService.EliminarCategoria(id);
            TempData["successMsg"] = "Categoría eliminada correctamente";
            return RedirectToAction("Categorias");
        }


        // -------------------- PRODUCTOS --------------------
        public async Task<IActionResult> Productos()
        {
            var productos = await _productoService.GetAllProductos();
            return View(productos);
        }

        [HttpGet]
        public async Task<IActionResult> EditarProducto(int id)
        {
            var producto = await _productoService.GetProductoById(id);
            if (producto == null)
                return NotFound();

            var categorias = await _categoriaService.GetAllCategorias();
            ViewBag.Categorias = categorias;

            var viewModel = new ProductoViewModel
            {
                Id = producto.Id,
                Titulo = producto.Titulo ?? "",
                Descripcion = producto.Descripcion ?? "",
                CategoriaId = producto.CategoriaId,
                Precio = producto.Precio,                 // directo
                Descuento = producto.Descuento,           // directo si es int
                PrecioConDescuento = (decimal)producto.PrecioConDescuento,
                Stock = producto.Stock,                   // directo
                IsActive = producto.IsActive,
                Imagen = producto.Imagen ?? "",
                Categorias = categorias
            };


            return View(viewModel);
        }



        [HttpGet]
        public async Task<IActionResult> AgregarProducto()
        {
            var categorias = await _categoriaService.GetAllCategorias();

            var model = new ProductoViewModel
            {
                Categorias = categorias
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> GuardarProducto(ProductoViewModel model)
        {
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(model.Id.ToString()), "Id");
            formData.Add(new StringContent(model.Titulo ?? ""), "Titulo");
            formData.Add(new StringContent(model.Descripcion ?? ""), "Descripcion");
            formData.Add(new StringContent(model.CategoriaId.ToString()), "CategoriaId");
            formData.Add(new StringContent(model.Precio.ToString()), "Precio");
            formData.Add(new StringContent(model.Stock.ToString()), "Stock");
            formData.Add(new StringContent(model.Descuento.ToString()), "Descuento"); // <--- importante
            formData.Add(new StringContent(model.IsActive.ToString()), "IsActive");

            if (model.File != null && model.File.Length > 0)
            {
                var stream = new StreamContent(model.File.OpenReadStream());
                stream.Headers.ContentType = new MediaTypeHeaderValue(model.File.ContentType);
                formData.Add(stream, "Imagen", model.File.FileName);
            }


            await _productoService.GuardarProducto(formData); // la API calcula el precio con descuento
            TempData["successMsg"] = "Producto guardado correctamente";
            return RedirectToAction("AgregarProducto");
        }



        [HttpPost]
        public async Task<IActionResult> ActualizarProducto(ProductoViewModel model)
        {
            var formData = new MultipartFormDataContent();

            formData.Add(new StringContent(model.Id.ToString()), "Id");
            formData.Add(new StringContent(model.Titulo ?? ""), "Titulo");
            formData.Add(new StringContent(model.Descripcion ?? ""), "Descripcion");
            formData.Add(new StringContent(model.CategoriaId.ToString()), "CategoriaId");
            formData.Add(new StringContent(model.Precio.ToString()), "Precio");
            formData.Add(new StringContent(model.Stock.ToString()), "Stock");
            formData.Add(new StringContent(model.Descuento.ToString()), "Descuento");
            formData.Add(new StringContent(model.IsActive.ToString()), "IsActive");

            // 👇 CLAVE: enviar imagen actual
            formData.Add(new StringContent(model.Imagen ?? ""), "Imagen");

            if (model.File != null && model.File.Length > 0)
            {
                var stream = new StreamContent(model.File.OpenReadStream());
                stream.Headers.ContentType =
                    new MediaTypeHeaderValue(model.File.ContentType);

                formData.Add(stream, "ImagenFile", model.File.FileName);
            }

            await _productoService.ActualizarProducto(formData);

            TempData["successMsg"] = "Producto actualizado correctamente";
            return RedirectToAction("EditarProducto", new { id = model.Id });
        }




        [HttpPost]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            await _productoService.EliminarProducto(id);
            TempData["successMsg"] = "Producto eliminado correctamente";
            return RedirectToAction("Productos");
        }
    }
}
