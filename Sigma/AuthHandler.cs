using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Sigma
{
    public class AuthHandler : AuthenticationHandler<AuthOptions>
    {
        protected AuthHandler(IOptionsMonitor<AuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override object Events { get => base.Events; set => base.Events = value; }

        protected override string ClaimsIssuer => base.ClaimsIssuer;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override Task<object> CreateEventsAsync()
        {
            return base.CreateEventsAsync();
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorizationHeader = Context.Request.Headers["Authorization"];
            if (!authorizationHeader.Any())
                return Task.FromResult(AuthenticateResult.NoResult());

            var value = authorizationHeader.ToString();
            if (string.IsNullOrWhiteSpace(value))
                return Task.FromResult(AuthenticateResult.NoResult());

            // place logic here to validate the header value (decrypt, call db etc)

            var claims = new[]
            {
                new Claim(System.Security.Claims.ClaimTypes.Name, "Bob")
            };

            // create a new claims identity and return an AuthenticationTicket 
            // with the correct scheme
            var claimsIdentity = new ClaimsIdentity(claims, Authentication.Scheme);

            var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties(), Authentication.Scheme);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            return base.HandleChallengeAsync(properties);
        }

        protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            return base.HandleForbiddenAsync(properties);
        }

        protected override Task InitializeEventsAsync()
        {
            return base.InitializeEventsAsync();
        }

        protected override Task InitializeHandlerAsync()
        {
            return base.InitializeHandlerAsync();
        }
    }
}
