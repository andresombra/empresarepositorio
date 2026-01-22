using GerEmpresa.Domain.Entities;

namespace Empresa.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task AddAsync(Usuario usuario);
        Task<Usuario?> AutenticarAsync(string login, string senha);
        Task<IList<Usuario>> ListaAsync();
    }
}
