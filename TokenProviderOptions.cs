using Microsoft.IdentityModel.Tokens;
using System;

namespace CookieLogin
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/Home/PostIndex";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(5);

        public SigningCredentials SigningCredentials { get; set; }
    }
}
