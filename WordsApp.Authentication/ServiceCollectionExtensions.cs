using Microsoft.Extensions.DependencyInjection;

namespace WordsApp.Authentication;

/// <summary>
/// Extensions to add auth services
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppUser(this IServiceCollection services)
    {
        return services.AddScoped<IWordsAppUser, WordsAppUser>();
    }
}