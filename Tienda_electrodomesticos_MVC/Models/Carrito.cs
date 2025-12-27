namespace Tienda_electrodomesticos_MVC.Models
{
    public class Carrito
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public int ProductoId { get; set; }

        public int Cantidad { get; set; }
        public double Precio { get; set; }

        // 🔹 Datos calculados que vienen desde la API
        public double PrecioTotal { get; set; }

        public double TotalPrecioOrdenes { get; set; }

        // 🔹 Opcional: si tu API luego devuelve info del producto
        public Producto? Producto { get; set; }
    }
}
