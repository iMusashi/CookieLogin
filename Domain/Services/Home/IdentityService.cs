using AutomobileCMS.Areas.Home.ViewModels;
using AutomobileCMS.Domain.Services.Home.Interfaces;
using AutomobileCMS.Infrastructure.ClientLibrary.ClientService;
using AutomobileCMS.Utilities.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileCMS.Domain.Services.Home
{
    public class IdentityService : IIdentityService
    {
        private readonly IdentityClient IdentityClient;
        private HttpContext HttpContext;
        private HttpClient Client;
        private readonly TokenValidationOptions TokenValidationOptions;

        public IdentityService(IdentityClient identityClient, IHttpContextAccessor httpContextAccessor,
            IOptions<TokenValidationOptions> optionsAccessor)
        {
            IdentityClient = identityClient;
            //HttpContextAccessor = httpContextAccessor;
            TokenValidationOptions = optionsAccessor.Value;
            HttpContext = httpContextAccessor.HttpContext;
            Client = IdentityClient.Client;
        }

        public async Task LoginAsync(LoginVM loginVM)
        {
            
            
            HttpRequestMessage message = GetHttpRequestMessage(loginVM);
            var response = await Client.SendAsync(message);

            var jwt = await response.Content.ReadAsStringAsync();

            string sanitizedJwt = CreateSanitizedJwtAndStoreInCookie(jwt);

            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(sanitizedJwt, tokenValidationParameters, out var securityToken);
                var properties = new AuthenticationProperties()
                {
                    //RedirectUri = "/"
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            SymmetricSecurityKey SecurityKey;
            try
            {
                SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenValidationOptions.Key));

            }
            catch (Exception ex)
            {
                SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Identity_Module_2.0"));
            }
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = TokenValidationOptions.Issuer,
                ValidAudience = TokenValidationOptions.Audience,
                IssuerSigningKey = SecurityKey
            };
        }

        /// <summary>
        /// Response jwt contains extra strings, which are replaced by empty space and sanitized. 
        /// This jwt is then stored in a cookie appended to the current response.
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        public string CreateSanitizedJwtAndStoreInCookie(string jwt)
        {
            string sanitizedJwt = "";
            if (jwt != null)
            {
                sanitizedJwt = jwt.Replace("\\", "").Trim(new char[1] { '"' });

                var now = DateTime.UtcNow.AddMinutes(330);
                // Serialize and return the response
                HttpContext.Response.ContentType = "application/json";
                HttpContext.Response.Cookies.Append(
                    ".AspNetCore.Jwt",
                    sanitizedJwt,
                    new CookieOptions()
                    {
                        Path = "/",
                        HttpOnly = true,
                    });

            }

            return sanitizedJwt;
        }

        public HttpRequestMessage GetHttpRequestMessage(LoginVM loginVM)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5001/api/values");
            var jsonObject = JsonConvert.SerializeObject(loginVM);
            HttpContent content = new StringContent(jsonObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            message.Content = content;
            return message;
        }

        public Task CallService()
        {
            
            return null;
        }
    }
}
