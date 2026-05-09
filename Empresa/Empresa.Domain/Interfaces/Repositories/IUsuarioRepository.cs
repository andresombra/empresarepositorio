using GerEmpresa.Domain.Entities;

namespace Empresa.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario?> AutenticarAsync(string login, string senha);
        Task<IList<Usuario>> ListaAsync();
    }
}
