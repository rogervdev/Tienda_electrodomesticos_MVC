namespace Tienda_electrodomesticos_MVC.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? ImagenNombre { get; set; }

        public bool IsActive { get; set; }
    }
}
