using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace WebApi_Projekt
{
    public class AuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public AuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base (options, logger, encoder, clock)
        {

        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            // Kollar om det ens finns en key
            if (!Request.Query.TryGetValue("api-key", out var potentialKey))
            {
                return AuthenticateResult.Fail("Missing Authorization Key");
            }

            var apiKey = "DemoToken";

            // kollar om keyn stämmer med apiKey
            if (!apiKey.Equals(potentialKey))
            {
                return AuthenticateResult.Fail("Invalid Api Key");
            }

            // annars lyckas resultatet
            var identity = new ClaimsIdentity(Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);

        }
    }
}
