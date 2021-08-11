using NSE.WebApp.MVC.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public interface IAutenticacaoService
    {
        Task<string> Login(UsuarioLogin usuarioLogin);
        Task<string> Registro(UsuarioRegistro usuarioRegistro);
    }

    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly HttpClient _httpClient;

        public AutenticacaoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Login(UsuarioLogin usuarioLogin)
        {
            var loginContent = SerializeContent(usuarioLogin);
            var response = await _httpClient.PostAsync("https://localhost:5001/api/identidade/autenticar", loginContent);
            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Registro(UsuarioRegistro usuarioRegistro)
        {
            var usuarioRegistroContent = SerializeContent(usuarioRegistro);
            var response = await _httpClient.PostAsync("https://localhost:5001/api/identidade/nova-conta", usuarioRegistroContent);
            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }

        private static StringContent SerializeContent(object content) => new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
    }
}
