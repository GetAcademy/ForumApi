using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ForumApi.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ForumApi.Authorization
{
    public static class CustomAuthenticationExtensions
    {
        public static AuthenticationBuilder AddCustomAuthentication(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<CustomAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<CustomAuthenticationOptions, AuthorizationHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
    public class CustomAuthenticationOptions : AuthenticationSchemeOptions
    {
        public ClaimsIdentity Identity { get; set; }
    }

    public class TfsoIdentity : ClaimsIdentity
    {
        public TfsoIdentity(Claim[] claims)
            :base(claims)
        {
        }

        public String TfsoUserId { get; set; }
    }

    public class AuthorizationHandler : AuthenticationHandler<CustomAuthenticationOptions>
    {
        private IUserRepository _userRepository;

        public AuthorizationHandler(
            IOptionsMonitor<CustomAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserRepository userRepository)
            : base(options, logger, encoder, clock)
        {
            _userRepository = userRepository;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Sjekke greier, hente ut identity, få tilbake userid
            // hvis man ikke finnner identity, sjekk request metode
            var identity = new TfsoIdentity(new[] { new Claim("role", "superuser") }) { TfsoUserId = "per" };
            if (identity == null)
            {
                if (Request.Method != "GET")
                    return Task.FromResult(
                        AuthenticateResult.Fail("Anonymous user can only use GET methods"));

                identity = new TfsoIdentity(new[] { new Claim("role", "anonymous") }) { TfsoUserId = "anonymous" };
            }
            else
            {
                //hvis man finner identity, gå til repo for å sjekke rolle
                //var user = _userRepository.Get(userId);
                //identity = new TfsoIdentity(new[] {new Claim("role", user.Role)}){ TfsoUserId = userId };
            }

            return Task.FromResult(
                AuthenticateResult.Success(
                    new AuthenticationTicket(
                        new ClaimsPrincipal(identity),
                        new AuthenticationProperties(),
                        "Basic")));
        }


    }
}
