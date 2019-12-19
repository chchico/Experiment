using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;   // GetUserManager
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

using WebApplication2.Models;

namespace WebApplication2.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
                else if (context.ClientId == "web")
                {
                    var expectedUri = new Uri(context.Request.Uri, "/");
                    context.Validated(expectedUri.AbsoluteUri);
                }
            }

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <remarks>
        /// post /token request. body grant_type=password&username=test&password=password
        /// call ValidateClientAuthentication
        /// call GrantResourceOwnerCredentials
        /// </remarks>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // パスワード認証の場合は、クライアントIDは提供されません
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            // 認証ロジックを書く
            if (context.UserName == "admin" && context.Password == "p@ssw0rd")
            {
                var user = new ApplicationUser();
                user.Id = "admin";
                user.UserName = "admin user";

                // context.Options.AuthenticationTypeを使ってClaimsIdentityを作る
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                // 必要なClaimを追加しておく。
                identity.AddClaims(new[]
                {
                    new Claim(ClaimTypes.GivenName, context.UserName),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.Role, "Admin")
                });

                var oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);
                var cookiesIdentity = await user.GenerateUserIdentityAsync(userManager, CookieAuthenticationDefaults.AuthenticationType);

                var properties = CreateProperties(user.UserName);
                var ticket = new AuthenticationTicket(oAuthIdentity, properties);
                context.Validated(ticket);

                // Authorize 属性を有効にする
                context.Request.Context.Authentication.SignIn(cookiesIdentity);
                
                // context.Validated(identity);
            }
            else
            {
                context.Rejected();
            }

            return;
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            var data = new Dictionary<string, string>
            {
                { "userName", userName }
            };

            return new AuthenticationProperties(data);
        }
    }
}