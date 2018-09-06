using AutomobileCMS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileCMS.Controllers
{
    public class HomeController : Controller
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; }
        private IHttpClientFactory HTTP;
        PRIVATE
        private readonly JwtTokenOptions JwtTokenOptions;

        public HomeController(IConfiguration configuration, IOptions<JwtTokenOptions> tokenOptions)
        {
            Configuration = configuration;
            JwtTokenOptions = tokenOptions;
        }

        public IActionResult Index()
        {
            //var user = HttpContext.User.Identity.IsAuthenticated;
            var vm = new LoginVM()
            {
                username = "Keshav",
                Password = "Hello",
                CurrentStatus = "Walkin Inward"
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult About()
        {
            //ViewData["Message"] = HttpContext.User.Identity.IsAuthenticated;
            //var user = HttpContext.User.Identity.IsAuthenticated;

            return Content("Keshav");
        }

        public IActionResult LogOut()
        {
            HttpContext.Response.Cookies.Delete("x");
            return RedirectToAction("About");
        }

        //[Authorize(Roles = "Admin")]
        //[Authorize(Policy = "CanAccessContact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> PostIndex(LoginVM loginVM)
        {// Update port # in the following line.
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5001/api/values");
            client.BaseAddress = new Uri("http://localhost:5001/");
            var xs = JwtTokenOptions.Issuer;
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));
            var data = new { username = "Keshav" };
            string jsonString = JsonConvert.SerializeObject(data);
            HttpContent content = new StringContent(jsonString);
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            message.Content = content;
            var 
            HttpResponseMessage postResponse = client.GetAsync("http://localhost:5001/api/values", content).Result;

            HttpResponseMessage postResponse = client.PostAsync("http://localhost:5001/api/values", content).Result;
            HttpResponseMessage sendResponse = client.SendAsync(message).Result;

            //var data = new { username = "Keshav" };
            //var json = JsonConvert.SerializeObject("Keshav");
            //var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            //HttpResponseMessage response = await client.PostAsJsonAsync("api/values", data);

            //Don't bother with this
            //HttpResponseMessage response = await client.PostAsync("api/values", stringContent);
            //HttpResponseMessage response = await client.GetAsync("api/values");

            var jwt = await postResponse.Content.ReadAsStringAsync();
            var sanitizedJWT = jwt.Replace("\\", "").Trim(new char[1] { '"' });
            var now = DateTime.UtcNow.AddMinutes(330);
            // Serialize and return the response
            HttpContext.Response.ContentType = "application/json";
            HttpContext.Response.Cookies.Append(
        ".AspNetCore.WmsJwt",
        sanitizedJWT,
        new CookieOptions()
        {
            Path = "/",
            HttpOnly = true,
        }
    );
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Configuration["Jwt:Issuer"],
                ValidAudience = Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            };
            SecurityToken securityToken;
            ClaimsPrincipal principal;
            try
            {
                var jwtString = tokenHandler.ReadJwtToken(sanitizedJWT);
                principal = new ClaimsPrincipal(new ClaimsIdentity(jwtString.Claims));
                //principal = tokenHandler.ValidateToken(jwtString, tokenValidationParameters, out securityToken);
            }
            catch (Exception)
            {
                principal = null;
            }
            try
            {
                //var jwtString = tokenHandler.ReadJwtToken(sanitizedJWT);
                principal = tokenHandler.ValidateToken(sanitizedJWT, tokenValidationParameters, out securityToken);
                var properties = new AuthenticationProperties()
                {
                    RedirectUri = "/"
                };
                var neededPrincipal =
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
            }
            catch (Exception)
            {
                principal = null;
            }
            //return Content("done");
            return RedirectToAction("About");
        }

        [HttpPut]
        public IActionResult UpdateIndex(LoginVM loginVM)
        {
            return View(loginVM);
        }
    }
}