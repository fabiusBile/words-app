using WordsApp.Persistence.ClientFactory;

namespace WordsApp.Persistence.Containers;

/// <summary>
/// Container for words and translations
/// </summary>
public class WordsContainer(ICosmosDbClientFactory clientFactory) : AbstractContainer(clientFactory, "words", "/user-id"), IWordsContainer;