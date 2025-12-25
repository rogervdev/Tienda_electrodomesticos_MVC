namespace Tienda_electrodomesticos_MVC.Models
{
    public class Producto
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public int CategoriaId { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public string? Imagen { get; set; }

        public int Descuento { get; set; }

        public decimal? PrecioConDescuento { get; set; }

        public bool IsActive { get; set; }

        // NUEVO: para mostrar el nombre de la categoría
        public string CategoriaNombre { get; set; } = string.Empty;
    }
}
