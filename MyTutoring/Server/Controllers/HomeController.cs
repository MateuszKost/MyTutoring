using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
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
        [Authorize(Roles = "admin, student, tutor")]
        public async Task<UserInfoModel> Index()
        {
            string id = HttpContext.User.FindFirstValue("id");
            string email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            string role = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            var url = await _storageContext.GetAsync(new FileContainer(), "GameDev_MKOKDL.pdf");

            return new UserInfoModel() { Id = id, Role = role };
        }
    }
}
