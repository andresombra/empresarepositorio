using Empresa.Application.Interfaces;
using Empresa.Application.Services;
using Empresa.Application.Validators;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Empresa.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IFornecedorService, FornecedorService>();
        services.AddScoped<IEmpresaService, EmpresaService>();

        // Validation
        services.AddValidatorsFromAssemblyContaining<UsuarioDtoValidator>();

        // Mapster
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
