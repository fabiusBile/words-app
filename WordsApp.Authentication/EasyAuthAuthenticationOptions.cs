using Microsoft.AspNetCore.Authentication;

namespace WordsApp.Authentication;

public class EasyAuthAuthenticationOptions : AuthenticationSchemeOptions
{
    public EasyAuthAuthenticationOptions()
    {
        Events = new object();
    }
}