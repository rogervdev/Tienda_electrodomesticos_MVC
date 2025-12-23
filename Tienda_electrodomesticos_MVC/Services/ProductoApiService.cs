using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Tienda_electrodomesticos_MVC.Models;

namespace Tienda_electrodomesticos_MVC.Services
{
    public class ProductoApiService
    {
        private readonly HttpClient _httpClient;

        public ProductoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: api/producto
        public async Task<List<Producto>> GetAllProductos()
        {
            var response = await _httpClient.GetAsync("api/producto");
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Producto>>(apiResponse)!;
        }

        // GET: api/producto/activos?categoria=...
        public async Task<List<Producto>> GetActiveProductos(string categoria = "")
        {
            var url = string.IsNullOrEmpty(categoria) ? "api/producto/activos" : $"api/producto/activos?categoria={categoria}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Producto>>(apiResponse)!;
        }

        // GET: api/producto/{id}
        public async Task<Producto?> GetProductoById(int id)
        {
            var response = await _httpClient.GetAsync($"api/producto/{id}");
            if (!response.IsSuccessStatusCode) return null;
            var apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Producto>(apiResponse);
        }

        // GET: api/producto/buscar?query=...
        public async Task<List<Producto>> BuscarProducto(string query)
        {
            var response = await _httpClient.GetAsync($"api/producto/buscar?query={query}");
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Producto>>(apiResponse)!;
        }

        // Para POST y PUT con FormData (multipart/form-data)
        public async Task<Producto> GuardarProducto(MultipartFormDataContent formData)
        {
            var response = await _httpClient.PostAsync("api/producto", formData);
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Producto>(apiResponse)!;
        }

        public async Task<Producto> ActualizarProducto(MultipartFormDataContent formData)
        {
            var response = await _httpClient.PutAsync("api/producto", formData);
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Producto>(apiResponse)!;
        }

        // DELETE: api/producto/{id}
        public async Task<bool> EliminarProducto(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/producto/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
