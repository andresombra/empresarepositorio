using System.Net.Http.Json;

namespace Empresa.WebApp.Services
{
    public class TokenService : ITokenService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string? _token;

        public TokenService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
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

                var loginRequest = new { username = $"{username}", password = $"{password}" };
                var response = await _httpClient.PostAsJsonAsync("api/auth/token", loginRequest);

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
            public string? Token { get; set; }
        }
    }
}