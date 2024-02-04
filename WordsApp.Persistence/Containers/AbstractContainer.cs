using Microsoft.Azure.Cosmos;
using WordsApp.Persistence.ClientFactory;

namespace WordsApp.Persistence.Containers;

/// <summary>
/// Abstractions to work with containers
/// </summary>
public abstract class AbstractContainer
{
    private const string DATABASE_NAME = "words";
    
    private readonly CosmosClient _client;
    private readonly string _containerName;
    private readonly string _partitionKeyPath;

    protected AbstractContainer(ICosmosDbClientFactory clientFactory, string containerName, string partitionKeyPath)
    {
        _client = clientFactory.GetClient();
        _containerName = containerName;
        _partitionKeyPath = partitionKeyPath;
    }

    public async Task<Container> GetContainer()
    {
        var db = await _client.CreateDatabaseIfNotExistsAsync(DATABASE_NAME);
        var container = await 
            db.Database.CreateContainerIfNotExistsAsync(id: _containerName, partitionKeyPath: _partitionKeyPath);

        return container;
    }
}