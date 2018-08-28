using CookieLogin.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CookieLogin.Controllers
{
    public class HomeController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private LoginController _login;

        public HomeController(LoginController login)
        {
            _login = login;
            //_signInManager = signInManager;
            //_userManager = userManager;
        }

        public IActionResult Index()
        {
            var user = HttpContext.User.Identity.IsAuthenticated;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            var user = HttpContext.User.Identity.IsAuthenticated;

            return View();
        }

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
        {
            
            var tokenString = _login.Login(loginVM) ;
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var token = tokenString.Value.ToString();
            //var jwttoken = tokenHandler.ReadJwtToken(token);
            //var claims = jwttoken.Claims;
            //var claimidentity = new ClaimsIdentity(claims);
            //List<ClaimsIdentity> claimsIdentity = new List<ClaimsIdentity>();
            //claimsIdentity.Add(claimidentity);
            //var principal = new ClaimsPrincipal(claimsIdentity);

            //await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, principal);
            //var auth = HttpContext.Response.Body;
            //var iden = HttpContext.User.Identity.IsAuthenticated;
            return Content("Done");
        }

        [HttpPut]
        public IActionResult UpdateIndex(LoginVM loginVM)
        {
            return View(loginVM);
        }
    }
}
