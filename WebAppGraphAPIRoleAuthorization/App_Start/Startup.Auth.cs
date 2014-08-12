namespace WebAppGraphAPIRoleAuthorization
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.Azure.ActiveDirectory.GraphClient;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OpenIdConnect;
    using Owin;

    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            var clientId = Settings.ClientId;
            var redirectUri = Settings.RedirectUri;
            var authority = Settings.Authority;

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(
                new CookieAuthenticationOptions
                {
                    Provider = new CookieAuthenticationProvider
                    {
                        OnResponseSignIn = context =>
                        {
                            var identitiy = context.Identity;
                            var accessToken = HttpContext.Current.Items[Settings.AccessTokenKey] as string;
                            var memberIdClaim = identitiy.FindFirst(Settings.ObjectIdentifierClaimType);
                            if (!string.IsNullOrWhiteSpace(accessToken) && memberIdClaim != null)
                            {
                                var memberId = memberIdClaim.Value;
                                var graphConnection = CreateGraphConnection(accessToken);
                                if (graphConnection.IsMemberOf(Settings.UsersGroupId, memberId))
                                {
                                    identitiy.AddClaim(new Claim(ClaimTypes.Role, Settings.UsersRole, ClaimValueTypes.String, Settings.RoleClaimIssuer));
                                }

                                if (graphConnection.IsMemberOf(Settings.AuditGroupId, memberId))
                                {
                                    identitiy.AddClaim(new Claim(ClaimTypes.Role, Settings.AdminRole, ClaimValueTypes.String, Settings.RoleClaimIssuer));
                                }
                            }
                        }
                    }
                });

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    Authority = authority,
                    PostLogoutRedirectUri = redirectUri,
                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        AuthorizationCodeReceived = context =>
                        {
                            var code = context.Code;
                            var appKey = Settings.Password;
                            var credential = new ClientCredential(clientId, appKey);
                            var graphResourceId = Settings.GraphResourceID;
                            var authContext = new AuthenticationContext(authority);

                            var result = authContext.AcquireTokenByAuthorizationCode(
                                code,
                                new Uri(redirectUri),
                                credential,
                                graphResourceId);

                            // Save the Azure AD access token in the context of the request.
                            HttpContext.Current.Items[Settings.AccessTokenKey] = result.AccessToken;

                            return Task.FromResult(0);
                        }
                    }
                });
        }

        private static GraphConnection CreateGraphConnection(string accessToken)
        {
            // Create graph connection with our access token and API version.
            var clientRequestId = Guid.NewGuid();
            var graphApiVersion = Settings.GraphApiVersion;
            var graphSettings = new GraphSettings { ApiVersion = graphApiVersion };

            return new GraphConnection(accessToken, clientRequestId, graphSettings);
        }
    }
}
