using Empresa.Domain.Interfaces.Repositories;
using Empresa.Domain.Interfaces;
using Empresa.Infrastructure.Data;
using Empresa.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Empresa.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EmpresaDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));

        // Repositories
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IFornecedorRepository, FornecedorRepository>();
        services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
