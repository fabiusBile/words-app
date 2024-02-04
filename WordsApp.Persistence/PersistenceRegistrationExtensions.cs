using Microsoft.Extensions.DependencyInjection;
using WordsApp.Persistence.ClientFactory;
using WordsApp.Persistence.Containers;
using WordsApp.Persistence.Repository;

namespace WordsApp.Persistence;

/// <summary>
/// Extensions for registering Azure CosmosDB persistence 
/// </summary>
public static class PersistenceRegistrationExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services
            .AddSingleton<ICosmosDbClientFactory, CosmosDbClientFactory>()
            .AddSingleton<IWordsContainer, WordsContainer>()
            .AddScoped<IWordsRepository, WordsRepository>();
    }
}