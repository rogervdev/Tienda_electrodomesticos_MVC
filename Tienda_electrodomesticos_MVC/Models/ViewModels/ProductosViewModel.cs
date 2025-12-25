namespace Tienda_electrodomesticos_MVC.Models.ViewModels
{
    public class ProductosViewModel
    {
       
        public List<Producto> Productos { get; set; } = new();
        public string CategoriaSeleccionada { get; set; } = "Todos los productos";

    }
}
