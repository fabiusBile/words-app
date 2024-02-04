using WordsApp.Domain.Models;

namespace WordsApp.Persistence.Repository;

public interface IWordsRepository
{
    Task AddOrUpdate(Word word, CancellationToken cancellationToken);
}