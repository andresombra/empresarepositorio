using Empresa.WebApp.Components;
using Empresa.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ✅ Registrar cliente de autenticação (sem AuthorizationHandler) e TokenService
var baseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:5000";
builder.Services.AddHttpClient("AUTH", client =>
{
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddScoped<ITokenService, TokenService>();

// ✅ Registrar AuthorizationHandler
builder.Services.AddTransient<AuthorizationHandler>();

// ✅ Registrar HttpClient com o interceptor
builder.Services.AddHttpClient("API", client =>
{
    var baseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:5000";
    client.BaseAddress = new Uri(baseUrl);
})
.AddHttpMessageHandler<AuthorizationHandler>();

// ✅ Registrar HttpClient padrão com factory
builder.Services.AddScoped(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("API");
});

var app = builder.Build();

// ✅ Inicializar token na startup
using (var scope = app.Services.CreateScope())
{
    try
    {
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        await tokenService.InitializeAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao inicializar token: {ex.Message}");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (!app.Environment.IsDevelopment())
{
    //app.UseHttpsRedirection();
}

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
