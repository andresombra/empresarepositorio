namespace Empresa.WebApp.Services
{
    public interface ITokenService
    {
        Task InitializeAsync();
        Task<string> GetTokenAsync();
    }
}