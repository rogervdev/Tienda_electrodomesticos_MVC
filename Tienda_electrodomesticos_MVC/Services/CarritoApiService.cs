using Newtonsoft.Json;
using System.Net.Http.Json;
using Tienda_electrodomesticos_MVC.Models;
using Tienda_electrodomesticos_MVC.Models.DTOs;

namespace Tienda_electrodomesticos_MVC.Services
{
    public class CarritoApiService
    {
        private readonly HttpClient _httpClient;

        public CarritoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: api/carrito/{usuarioId}
        public async Task<List<Carrito>> ObtenerCarritoPorUsuario(int usuarioId)
        {
            var response = await _httpClient.GetAsync($"api/carrito/{usuarioId}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Carrito>>(json)!;
        }

        // POST: api/carrito
        public async Task<Carrito> AgregarAlCarrito(int usuarioId, int productoId)
        {
            var dto = new CarritoRequestDto
            {
                UsuarioId = usuarioId,
                ProductoId = productoId
            };

            var response = await _httpClient.PostAsJsonAsync("api/carrito", dto);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Carrito>(json)!;
        }

        // GET: api/carrito/contar/{usuarioId}
        public async Task<int> ContarCarrito(int usuarioId)
        {
            var response = await _httpClient.GetAsync($"api/carrito/contar/{usuarioId}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<int>(json)!;
        }

        // DELETE: api/carrito/{usuarioId}/{productoId}
        public async Task<bool> EliminarProducto(int usuarioId, int productoId)
        {
            var response = await _httpClient.DeleteAsync($"api/carrito/{usuarioId}/{productoId}");
            return response.IsSuccessStatusCode;
        }

        // PUT: api/carrito/aumentar/{usuarioId}/{productoId}
        public async Task<bool> AumentarCantidad(int usuarioId, int productoId)
        {
            var response = await _httpClient.PutAsync($"api/carrito/aumentar/{usuarioId}/{productoId}", null);
            return response.IsSuccessStatusCode;
        }

        // PUT: api/carrito/disminuir/{usuarioId}/{productoId}
        public async Task<bool> DisminuirCantidad(int usuarioId, int productoId)
        {
            var response = await _httpClient.PutAsync($"api/carrito/disminuir/{usuarioId}/{productoId}", null);
            return response.IsSuccessStatusCode;
        }

    }
}
