using AutomobileCMS.Areas.Home.ViewModels;
using AutomobileCMS.Domain.Services.Home.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutomobileCMS.Areas.Home.Controllers
{
    [Area("Home")]
    public class IdentityController : Controller
    {
        private readonly IIdentityService IdentityService;

        public IdentityController(IIdentityService identityService)
        {
            IdentityService = identityService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Index(LoginVM loginVM)
        {
            var loginStatus = new LoginStatus();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                //loginStatus.Message = "Invalid Username or Password";
                //loginStatus.Success = false;
                //loginStatus.TargetURL = "";
                ////return Json(loginStatus);
                return View("Index", loginVM);
            }
            ModelState.AddModelError("", "Invalid Username or Password");
            return View("Index", loginVM);
            //loginStatus.Message = "Login Successful";
            //loginStatus.Success = true;
            //loginStatus.TargetURL = Url.Action(nameof(Service));
            //return Json(loginStatus);
            //await IdentityService.LoginAsync(loginVM);
            //return RedirectToAction("Service");
        }

        public IActionResult Service()
        {
            var vm = new ServiceVM();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Service(ServiceVM serviceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View(serviceVM);
            }

            ModelState.AddModelError("", "Invalid Username or Password");

            //var user = HttpContext.User;
            //var result = IdentityService.CallService();
            return View();
        }

        //[HttpPost]
        //public IActionResult Service(ServiceVM serviceVM)
        //{
        //    var vm = new ServiceVM();
        //    return View(vm);
        //}

    }
    public class LoginStatus
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string TargetURL { get; set; }
    }
}
