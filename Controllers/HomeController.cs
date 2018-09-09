using AutomobileCMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutomobileCMS.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration Configuration { get; }
        private readonly JwtTokenOptions JwtTokenOptions;

        public HomeController(IConfiguration configuration, IOptions<JwtTokenOptions> optionsAccessor)
        {
            Configuration = configuration;
            JwtTokenOptions = optionsAccessor.Value;
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
        {
            return RedirectToAction("About");
        }

        [HttpPut]
        public IActionResult UpdateIndex(LoginVM loginVM)
        {
            return View(loginVM);
        }
    }
}