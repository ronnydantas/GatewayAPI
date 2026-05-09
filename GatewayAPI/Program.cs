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

        // Configuração de serviços
        builder.Services
            .AddSwaggerConfig(builder.Configuration)
            .AddControllers();

        builder.Services.AddCustomCors();


        // Depois registre seus repositórios/serviços que dependem de SignInManager/UserManager
       // builder.Services.AddRepository(builder.Configuration);

        // Registrar serviços e MediatR do domínio
       // builder.Services.AddDomain(builder.Configuration);

        // Registrar autenticação JWT (separado)
        builder.Services.AddJwtAuthentication(builder.Configuration);

        var app = builder.Build();

        // Ajuste do caminho base para o nome correto do projeto
        app.UsePathBase("/gateway-api");

        app.UseCustomCors();

        app.UseRouting();

        // Swagger
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/gateway-api/swagger/v1/swagger.json", "Gateway API V1");
            options.RoutePrefix = string.Empty;
        });

        // Adiciona autenticação e autorização JWT
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}