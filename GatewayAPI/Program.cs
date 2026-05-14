using Domain;
using Domain.Services;
using Domain.Services.Interfaces;
using GatewayAPI.Extensions;
using GatewayAPI.Extensions.SwaggerConfigurations;


/// <summary>
/// Classe principal do aplicativo Gateway API.
/// </summary>
public class Program
{
    /// <summary>
    /// Ponto de entrada principal do aplicativo.
    /// </summary>
    /// <param name="args">Argumentos de linha de comando.</param>
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // =========================================
        // Controllers + Swagger
        // =========================================
        builder.Services
            .AddControllers();

        builder.Services
            .AddSwaggerConfig(builder.Configuration);

        // =========================================
        // CORS
        // =========================================
        builder.Services.AddCustomCors();

        // =========================================
        // MediatR / Domain
        // =========================================
        builder.Services.AddDomain(builder.Configuration);

        // =========================================
        // JWT Authentication
        // =========================================
        builder.Services.AddJwtAuthentication(builder.Configuration);

        // =========================================
        // HttpContextAccessor
        // =========================================
        builder.Services.AddHttpContextAccessor();

        // =========================================
        // Token Accessor
        // =========================================
        builder.Services.AddScoped<ITokenAccessor, TokenAccessor>();

        // =========================================
        // Reverse Proxy (YARP)
        // =========================================
        builder.Services
            .AddReverseProxy()
            .LoadFromConfig(
                builder.Configuration.GetSection("ReverseProxy"));

        // =========================================
        // Identity API
        // =========================================
        builder.Services.AddHttpClient<IIdentityService, IdentityService>(
            client =>
            {
                client.BaseAddress = new Uri(
                    builder.Configuration["Services:IdentityApi"]!);
            });

        // =========================================
        // User Service
        // =========================================
        builder.Services.AddHttpClient<IPersonService, PersonService>(
            client =>
            {
                client.BaseAddress = new Uri(
                    builder.Configuration["Services:UserService"]!);
            });

        var app = builder.Build();

        // =========================================
        // Base Path
        // =========================================
        app.UsePathBase("/gateway-api");

        // =========================================
        // Swagger
        // =========================================
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(
                "/gateway-api/swagger/v1/swagger.json",
                "Gateway API V1");

            options.RoutePrefix = string.Empty;
        });

        // =========================================
        // Middlewares
        // =========================================
        app.UseCustomCors();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        // =========================================
        // Controllers
        // =========================================
        app.MapControllers();

        // =========================================
        // Reverse Proxy
        // =========================================
        app.MapReverseProxy();

        await app.RunAsync();
    }
}