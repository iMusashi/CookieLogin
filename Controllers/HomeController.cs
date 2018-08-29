using CookieLogin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CookieLogin.Controllers
{
    public class HomeController : Controller
    {
        private LoginController _login;
        static HttpClient client = new HttpClient();
        public HomeController(LoginController login)
        {
            _login = login;
        }

        public IActionResult Index()
        {
            var user = HttpContext.User.Identity.IsAuthenticated;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = HttpContext.User.Identity.IsAuthenticated;
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
        {// Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:5001/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var data = new { username = "Keshav" };
            var json = JsonConvert.SerializeObject("Keshav");
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsJsonAsync("api/values", data);
            //HttpResponseMessage response = await client.PostAsync("api/values", stringContent);
            //HttpResponseMessage response = await client.GetAsync("api/values");
            var jwt = await response.Content.ReadAsStringAsync();
            var sanitizedJWT = jwt.Replace("\\", "").Trim(new char[1] { '"' });
            var now = DateTime.UtcNow.AddMinutes(330);
            // Serialize and return the response
            HttpContext.Response.ContentType = "application/json";
            HttpContext.Response.Cookies.Append(
        "x",
        sanitizedJWT,
        new CookieOptions()
        {
            Path = "/",
            HttpOnly = true,
        }
    );

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
