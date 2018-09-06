using AutomobileCMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AutomobileCMS.Controllers
{
    public class SecurityController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public IActionResult InsertDetails(SecurityEntry securityEntry)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var securityDetailsVM = new SecurityDetails
            {
                Text = "Loaded via Search Action"
            };
            //var time = new TimeSpan(0, 0, 1);
            //Thread.Sleep(time);
            return PartialView(securityDetailsVM);
        }

        public PartialViewResult CreateDetails()
        {
            var securityDetailsVM = new SecurityDetails
            {
                Text = "Loaded via Create Action"
            };
            
            var time = new TimeSpan(0, 0, 3);
            Thread.Sleep(time);
            return PartialView(nameof(InsertDetails), securityDetailsVM);
        }


        public async Task<IActionResult> PostVehicleDetailsAsync(HttpClient httpClient)
        {
            //httpClient.BaseAddress= 

                return null;
        }
    }
}