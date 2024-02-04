using Microsoft.Extensions.DependencyInjection;
using WordsApp.Domain.User;

namespace WordsApp.Authentication;

/// <summary>
/// Extensions to add auth services
/// </summary>
public static class AuthenticationRegistrationExtensions
{
    /// <summary>
    /// Registers app user abstraction
    /// </summary>
    public static IServiceCollection AddAppUser(this IServiceCollection services)
    {
        return services.AddScoped<IWordsAppUser, WordsAppUser>();
    }
}