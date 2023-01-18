using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using Zephry;

namespace Grandmark

{
    // Authentication Scheme
    public class GrandmarkSchemeOptions : AuthenticationSchemeOptions
    {
    }

    // Authentication Handler
    public class GrandmarkAuthenticationHandler : AuthenticationHandler<GrandmarkSchemeOptions>
    {
        public GrandmarkAuthenticationHandler(
            IOptionsMonitor<GrandmarkSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Get out if anonymous
            Endpoint? m = Request.HttpContext.GetEndpoint();
            if (m != null && m.Metadata.Any(m => m is AllowAnonymousAttribute))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            // Authenticate Cookie
            try
            {
                var vLogonToken = Utils.GetLogonToken(Request);
                // Do user auth here
                return Task.FromResult(AuthenticateResult.NoResult());
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.FromResult(AuthenticateResult.Fail(string.Format("Cookie deserialization failed: {0}", ex.Message)));
            }
        }

    }

}
