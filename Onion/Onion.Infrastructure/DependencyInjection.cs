using Microsoft.Extensions.DependencyInjection;
using Onion.Application.Interfaces;
using Onion.Infrastructure.Repositories;

namespace Onion.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Singleton because the store is in memory and must survive between
        // requests. A database-backed repository would be scoped.
        services.AddSingleton<IGreetingRepository, InMemoryGreetingRepository>();

        return services;
    }
}
