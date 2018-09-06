using AutomobileCMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutomobileCMS.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public LoginController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginVM loginVM)
        {
            IActionResult response = Unauthorized();

            var user = AuthenticateUser(loginVM);

            if (user != null)
            {
                var claims = GetClaims(user);
                var log = Configuration["Logging:LogLevel:Default"];
                var issuer = Configuration["Jwt:Issuer"];
                var audience = Configuration["Jwt:Audience"];
                var key = Configuration["Jwt:Key"];
                var tokenString = GenerateJSONWebToken(claims, issuer, audience, key);
                //await _signInManager.PasswordSignInAsync("Keshav", "Keshav", true, false);
                //var prop = new AuthenticationProperties();
                //await HttpContext.SignInAsync(HttpContext, JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claims), prop);
                //await HttpContext.SignInAsync( new ClaimsPrincipal(claims));
                Response.Cookies.Append(
        "x",
        tokenString,
        new CookieOptions()
        {
            Path = "/"
        }
    );

                response = Ok(tokenString);
            }
            return response;
        }

        private string GenerateJSONWebToken(ClaimsIdentity claims, string issuer, string audience, string key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
            return tokenHandler.WriteToken(tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = claims,
                SigningCredentials = signingCredentials,
                Expires = DateTime.Now.AddMinutes(20)
            }));
        }

        private ApplicationUser AuthenticateUser(LoginVM loginVM)
        {
            ApplicationUser user = null;

            if (loginVM.username == "Keshav")
            {
                user = new ApplicationUser { UserName = "Keshav" };
            }
            return user;
        }

        private ClaimsIdentity GetClaims(ApplicationUser appUser)
        {
            ClaimsIdentity claims = new ClaimsIdentity();
            string[] roles = new string[] { "Admin" };
            string Roles = string.Join(',', roles);
            claims.AddClaim(new Claim(ClaimTypes.Name, appUser.UserName));
            claims.AddClaim(new Claim(ClaimTypes.Role, Roles));

            return claims;
        }
    }
}