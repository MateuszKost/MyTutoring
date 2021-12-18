using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using MyTutoring.BlobStorageManager.Containers;
using MyTutoring.BlobStorageManager.Context;
using System.Security.Claims;

namespace MyTutoring.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : Controller
    { 
        private IConfiguration _configuration;
        private readonly IStorageContext<IStorageContainer> _storageContext;
        public HomeController(IConfiguration configuration, IStorageContext<IStorageContainer> storageContext)
        {
            _configuration = configuration;
            _storageContext = storageContext;
        }

        [HttpGet("test")]
        [Authorize(Roles = "admin, student, teacher")]
        public async Task<TestModel> Index()
        {
            string id = HttpContext.User.FindFirstValue("id");
            string email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            string role = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            var imgBytes = await _storageContext.GetAsync(new FileContainer(), "GameDev_MKOKDL.pdf");

            string tmp = Convert.ToBase64String(imgBytes);

            return new TestModel() { Id = id, Name = email, Role = role };
        }
    }
}
