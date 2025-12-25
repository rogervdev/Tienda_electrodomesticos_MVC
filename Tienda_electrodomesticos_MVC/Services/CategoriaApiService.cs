using Newtonsoft.Json;
using System.Net.Http;
using Tienda_electrodomesticos_MVC.Models;
using Tienda_electrodomesticos_MVC.Models.ViewModels;


namespace Tienda_electrodomesticos_MVC.Services
{
    public class CategoriaApiService
    {
        private readonly HttpClient _httpClient;

        public CategoriaApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: api/categoria
        public async Task<List<Categoria>> GetAllCategorias()
        {
            var response = await _httpClient.GetAsync("api/categoria");
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Categoria>>(apiResponse)!;
        }

        // GET: api/categoria/activos
        public async Task<List<Categoria>> GetAllActiveCategorias()
        {
            var response = await _httpClient.GetAsync("api/categoria/activos");
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Categoria>>(apiResponse)!;
        }

        // GET: api/categoria/{id}
        public async Task<Categoria?> GetCategoriaById(int id)
        {
            var response = await _httpClient.GetAsync($"api/categoria/{id}");
            if (!response.IsSuccessStatusCode) return null;
            var apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Categoria>(apiResponse);
        }

        // POST: api/categoria
        // POST: api/categoria con imagen
        public async Task<Categoria> GuardarCategoria(MultipartFormDataContent formData)
        {
            var response = await _httpClient.PostAsync("api/categoria", formData);
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Categoria>(apiResponse)!;
        }



        // PUT: api/categoria/{id}
        public async Task<Categoria> ActualizarCategoria(int id, MultipartFormDataContent formData)
        {
            var response = await _httpClient.PutAsync($"api/categoria/{id}", formData);
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Categoria>(apiResponse)!;
        }


        // DELETE: api/categoria/{id}
        public async Task<bool> EliminarCategoria(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/categoria/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
