using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Tienda_electrodomesticos_MVC.Models;

namespace Tienda_electrodomesticos_MVC.Models.ViewModels
{
    public class ProductoViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public decimal Precio { get; set; }
        public int Descuento { get; set; }
        public decimal PrecioConDescuento { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public string Imagen { get; set; } = string.Empty;

        // Para subir nueva imagen
        public IFormFile? File { get; set; }

        // Lista de categorías para el select
        public List<Categoria> Categorias { get; set; } = new List<Categoria>();

    }
}
