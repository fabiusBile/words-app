using Microsoft.Azure.Cosmos;

namespace WordsApp.Persistence.ClientFactory;

/// <summary>
/// Factory for obtaining azure cosmos db client
/// </summary>
public interface ICosmosDbClientFactory
{
    /// <summary>
    /// Gets configured cosmos client
    /// </summary>
    CosmosClient GetClient();
}