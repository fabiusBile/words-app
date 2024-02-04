using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WordsApp.Domain.Models;
using WordsApp.Persistence.Repository;

namespace WordsApp;

/// <summary>
/// User words management
/// </summary>
[ApiController]
[Route("[controller]")]
[Authorize] 
public class WordsController : ControllerBase
{
    private readonly IWordsRepository _wordsRepository;

    public WordsController(IWordsRepository wordsRepository)
    {
        _wordsRepository = wordsRepository;
    }

    [HttpPost]
    public async Task<ActionResult> AddOrUpdate(Word word, CancellationToken cancellationToken)
    {
        await _wordsRepository.AddOrUpdate(word, cancellationToken);

        return Ok();
    }
}