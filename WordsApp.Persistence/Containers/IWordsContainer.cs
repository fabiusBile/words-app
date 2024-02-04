using Microsoft.Azure.Cosmos;

namespace WordsApp.Persistence.Containers;

public interface IWordsContainer
{
    Task<Container> GetContainer();
}