namespace Tienda_electrodomesticos_MVC.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Categoria> Categorias { get; set; } = new();
        public List<Producto> Productos { get; set; } = new();
    }

}
