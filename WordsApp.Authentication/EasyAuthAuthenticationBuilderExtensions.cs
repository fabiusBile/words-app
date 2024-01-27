using Microsoft.AspNetCore.Authentication;
using WordsApp.Authentication.Internals;

namespace WordsApp.Authentication;

/// <summary>
/// Support for EasyAuth authentication in Azure Container Apps
/// </summary>
public static class EasyAuthAuthenticationBuilderExtensions
{
    public const string EASYAUTHSCHEMENAME = "EasyAuth";

    public static AuthenticationBuilder AddAzureContainerAppsEasyAuth(
        this AuthenticationBuilder builder,
        Action<EasyAuthAuthenticationOptions>? configure = null)
    {
        configure ??= o => { };

        return builder.AddScheme<EasyAuthAuthenticationOptions, EasyAuthAuthenticationHandler>(
            EASYAUTHSCHEMENAME,
            EASYAUTHSCHEMENAME,
            configure);
    }
}