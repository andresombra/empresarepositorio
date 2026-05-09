using System.Net.Http.Json;
using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Empresa.WebApp.Services
{
    public class TokenService : ITokenService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private string? _token;

        public TokenService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task InitializeAsync()
        {
            try
            {
                var username = _configuration["ApiSettings:Username"];
                var password = _configuration["ApiSettings:Password"];

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    throw new Exception("Credenciais não configuradas no appsettings.json");

                // Use a dedicated client for authentication to avoid circular DI between
                // the authorization handler and the token service.
                var clientName = _configuration["ApiSettings:AuthClientName"] ?? "AUTH";
                var client = _httpClientFactory.CreateClient(clientName);

                var loginRequest = new { username = username, password = password };
                var response = await client.PostAsJsonAsync("api/auth/token", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                    _token = result?.Token;
                }
                else
                {
                    throw new Exception($"Erro ao gerar token: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro na autenticação: {ex.Message}", ex);
            }
        }

        public Task<string> GetTokenAsync()
        {
            return Task.FromResult(_token ?? string.Empty);
        }

        private class TokenResponse
        {
            [JsonPropertyName("token")]
            public string? Token { get; set; }
        }
    }
}