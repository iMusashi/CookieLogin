using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;

namespace AutomobileCMS.Infrastructure.ClientLibrary.ClientService
{
    public class IdentityClient
    {
        public HttpClient Client { get; }
        public HttpContext HttpContext;
        public IdentityClient(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            HttpContext = httpContextAccessor.HttpContext;
            client.BaseAddress = new Uri("http://localhost:5001/");
            var IsJwtCookieExists = HttpContext.Request.Cookies.ContainsKey(".AspNetCore.Jwt");
            string cookie = "";
            if (IsJwtCookieExists)
            {
                cookie = HttpContext.Request.Cookies[".AspNetCore.Jwt"];
            }
            client.DefaultRequestHeaders.Add("Authorization",
                "Bearer " + cookie);
            // Accepted response type
            client.DefaultRequestHeaders.Add("Accept",
                "application/json");
            //TODO: Retrieve and append token here.
            Client = client;
        }
    }
}
