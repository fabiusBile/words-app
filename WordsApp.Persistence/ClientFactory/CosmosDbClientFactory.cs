using System.Configuration;
using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace WordsApp.Persistence.ClientFactory;

/// <summary>
/// Factory for obtaining azure cosmos db client
/// </summary>
public class CosmosDbClientFactory : ICosmosDbClientFactory
{
    private readonly string _endpoint;
    
    public CosmosDbClientFactory(IConfiguration configuration)
    {
        _endpoint = configuration["AZURE_COSMOS_DB_NOSQL_ENDPOINT"] 
                    ?? throw new ConfigurationErrorsException("Missing AZURE_COSMOS_DB_NOSQL_ENDPOINT in configuration");
    }

    /// <summary>
    /// Gets configured cosmos client
    /// </summary>
    public CosmosClient GetClient() =>
        new(
            accountEndpoint: _endpoint,
            tokenCredential: new DefaultAzureCredential()
        );
}