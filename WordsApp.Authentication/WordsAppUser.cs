using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Web;
using WordsApp.Domain.User;

namespace WordsApp.Authentication;

/// <summary>
/// Abstraction for app user
/// </summary>
public class WordsAppUser : IWordsAppUser
{
    public string UserName { get; }
    public Guid UserId { get; }
    public string? ProfilePicture { get; }
    public string AuthenticationType { get; }
    
    public WordsAppUser(IHttpContextAccessor contextAccessor)
    {
        var user = contextAccessor.HttpContext?.User;

        if (user == null)
            throw new AuthenticationException("User should be authorized to get claims");
        
        UserName = user.GetDisplayName()!;
        UserId = IdHasher.StringToGUID(user.GetNameIdentifierId()!);
        ProfilePicture = GetProfilePicture(user);
        AuthenticationType = user.Identity!.AuthenticationType!;
    }


    private static string? GetProfilePicture(ClaimsPrincipal user)
    {
        return user.Identity!.AuthenticationType switch
        {
            "github" => user.FindFirstValue("urn:github:avatar_url") ?? user.FindFirstValue("urn:github:gravatar_url"),
            _ => null
        };
    }
}