using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Claims;

namespace MyTutoring.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : Controller
    { 
        private IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("test")]
        [Authorize(Roles = "admin, student, teacher")]
        public async Task<TestModel> Index()
        {
            string id = HttpContext.User.FindFirstValue("id");
            string email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            string role = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            return new TestModel() { Id = id, Name = email, Role = role };
        }
    }
}
