using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using Tienda_electrodomesticos_MVC.Models;

namespace Tienda_electrodomesticos_MVC.Services
{
    public class UsuarioApiService
    {
        private readonly HttpClient _httpClient;

        public UsuarioApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // POST: api/usuario/registrar
        public async Task<Usuario> RegistrarUsuario(object usuarioRegistrarDto)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/usuario/registrar",
                usuarioRegistrarDto
            );

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Usuario>();
        }

        // GET: api/usuario/email?email=...
        public async Task<Usuario?> ObtenerPorEmail(string email)
        {
            var response = await _httpClient.GetAsync($"api/usuario/email?email={email}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Usuario>(json);
        }

        // GET: api/usuario/rol?rol=ROLE_USER
        public async Task<List<Usuario>> ListarUsuariosPorRol(string rol)
        {
            var response = await _httpClient.GetAsync($"api/usuario/rol?rol={rol}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Usuario>>(json)!;
        }

        // PUT: api/usuario/estado/{id}
        public async Task<bool> ActualizarEstadoCuenta(int id, bool estado)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"api/usuario/estado/{id}",
                estado
            );

            return response.IsSuccessStatusCode;
        }

        // PUT: api/usuario/reiniciar-contador/{id}
        public async Task ReiniciarContador(int id)
        {
            await _httpClient.PutAsync($"api/usuario/reiniciar-contador/{id}", null);
        }

        // PUT: api/usuario/actualizar
        public async Task<Usuario> ActualizarUsuario(Usuario usuario)
        {
            var response = await _httpClient.PutAsJsonAsync(
                "api/usuario/actualizar",
                usuario
            );

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Usuario>();
        }

        // PUT: api/usuario/reset-token
        public async Task ActualizarResetToken(string email, string resetToken)
        {
            var dto = new
            {
                Email = email,
                ResetToken = resetToken
            };

            await _httpClient.PutAsJsonAsync("api/usuario/reset-token", dto);
        }

        // GET: api/usuario/token?token=xyz
        public async Task<Usuario?> ObtenerPorToken(string token)
        {
            var response = await _httpClient.GetAsync($"api/usuario/token?token={token}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Usuario>(json);
        }
    }
}
