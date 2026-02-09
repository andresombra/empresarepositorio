using Empresa.Domain.Interfaces.Repositories;
using Empresa.Infrastructure.Data.Bases;
using Empresa.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Empresa.Infrastructure.Data.Configuration
{
    public static class DependencyInjectionConfigData
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddRepositories()
                .AddUnitOfWork();
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            return services;
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
