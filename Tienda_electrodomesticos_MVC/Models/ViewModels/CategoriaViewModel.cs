using Microsoft.AspNetCore.Http;

namespace Tienda_electrodomesticos_MVC.Models.ViewModels
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }  // útil para actualizar
        public string Nombre { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        // Este es el archivo de imagen que se sube desde el formulario
        public IFormFile? File { get; set; }

        // Esto sirve para mostrar la imagen actual en el formulario de edición
        public string? ImagenNombre { get; set; }
    }
}
