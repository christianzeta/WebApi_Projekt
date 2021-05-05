using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WebApi_Projekt.Data;

namespace WebApi_Projekt
{
    public class AuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly Context _context;

        public AuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            Context context)
            : base (options, logger, encoder, clock)
        {
            _context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            // Kollar om det ens finns en key
            if (!Request.Query.TryGetValue("token", out var potentialToken))
            {
                return AuthenticateResult.Fail("Missing Authorization Key");
            }

            string token = potentialToken.ToString();
            // kollar om keyn stämmer med apiKey
            if (_context.MyUsers.FirstOrDefault(t=>t.Token == token) != null)
            {
                // annars lyckas resultatet
                var identity = new ClaimsIdentity(Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Invalid Api Key");


        }
    }
}
