using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MyTutoring.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet("test")]
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            string id = HttpContext.User.FindFirstValue("id");
            string email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            string role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
            return Ok();
        }
    }
}
