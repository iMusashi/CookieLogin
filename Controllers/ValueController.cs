using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CookieLogin.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ValueController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            var user = HttpContext.User;
            return new string[] { "value1", "value2" };
        }
    }
}
