using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using Tienda_electrodomesticos_MVC.Models;

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
            var response = await _httpClient.GetAsync($"{usuarioId}"); // solo el id
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Carrito>>(json)!;
        }

        // POST: api/carrito
        public async Task<Carrito> AgregarAlCarrito(int usuarioId, int productoId)
        {
            var dto = new
            {
                UsuarioId = usuarioId,
                ProductoId = productoId
            };

            var response = await _httpClient.PostAsJsonAsync("api/carrito", dto);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Carrito>();
        }

        // GET: api/carrito/contar/{usuarioId}
        public async Task<int> ContarCarrito(int usuarioId)
        {
            var response = await _httpClient.GetAsync($"contar/{usuarioId}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<int>(json);
        }
    }
}
