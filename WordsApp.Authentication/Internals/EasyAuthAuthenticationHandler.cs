using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WordsApp.Authentication.Internals;

internal class EasyAuthAuthenticationHandler : AuthenticationHandler<EasyAuthAuthenticationOptions>
{
    public EasyAuthAuthenticationHandler(
        IOptionsMonitor<EasyAuthAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            var easyAuthProvider = Context.Request.Headers["X-MS-CLIENT-PRINCIPAL-IDP"].FirstOrDefault() ?? "aad";
            var msClientPrincipalEncoded = Context.Request.Headers["X-MS-CLIENT-PRINCIPAL"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(msClientPrincipalEncoded))
                return AuthenticateResult.NoResult();

            var decodedBytes = Convert.FromBase64String(msClientPrincipalEncoded);
            using var memoryStream = new MemoryStream(decodedBytes);
            var clientPrincipal = await JsonSerializer.DeserializeAsync<MsClientPrincipal>(memoryStream);

            if (clientPrincipal == null || !clientPrincipal.Claims.Any())
                return AuthenticateResult.NoResult();

            var claims = clientPrincipal.Claims.Select(claim => new Claim(claim.Type, claim.Value)).ToArray();

            // remap "roles" claims from easy auth to the more standard ClaimTypes.Role / "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
            var easyAuthRoleClaims = claims.Where(claim => claim.Type == "roles");
            var claimsAndRoles = claims.Concat(easyAuthRoleClaims.Select(role => new Claim(ClaimTypes.Role, role.Value)));

            var principal = new ClaimsPrincipal();
            principal.AddIdentity(new ClaimsIdentity(claimsAndRoles, clientPrincipal.AuthenticationType, clientPrincipal.NameType, ClaimTypes.Role));

            var ticket = new AuthenticationTicket(principal, easyAuthProvider);
            var success = AuthenticateResult.Success(ticket);
            Context.User = principal;

            return success;
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail(ex);
        }
    }
}