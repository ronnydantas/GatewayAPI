using Domain.Services;
using Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class AddDomainSetup
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {

        // Register MediatR handlers from the domain assembly
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AddDomainSetup).Assembly));

        services.AddScoped<ITokenAccessor, TokenAccessor>();

        // Required by UserService to access the current HttpContext
        services.AddHttpContextAccessor();

        return services;
    }
}
