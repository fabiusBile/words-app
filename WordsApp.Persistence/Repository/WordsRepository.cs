using Microsoft.Azure.Cosmos;
using WordsApp.Domain.Models;
using WordsApp.Domain.User;
using WordsApp.Persistence.Containers;

namespace WordsApp.Persistence.Repository;

/// <summary>
/// Repository for user words
/// </summary>
public class WordsRepository : IWordsRepository
{
    private readonly IWordsAppUser _user;
    private readonly IWordsContainer _wordsContainer;

    public WordsRepository(IWordsAppUser user, IWordsContainer wordsContainer)
    {
        _user = user;
        _wordsContainer = wordsContainer;
    }

    public async Task AddOrUpdate(Word word, CancellationToken cancellationToken)
    {
        var container = await _wordsContainer.GetContainer();
        await container.UpsertItemAsync(
            item: word,
            partitionKey: new PartitionKey(_user.UserId.ToString()),
            cancellationToken: cancellationToken);
    }
}