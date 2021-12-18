using DataAccessLayer;
using DataEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Claims;

namespace MyTutoring.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MaterialsGroupController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _uow;

        public MaterialsGroupController(IConfiguration configuration)
        {
            _configuration = configuration;
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
        }

        //[HttpGet("Get")]
        //[Authorize(Roles = "student, teacher")]
        //public async Task<ActionResult<EditProfileModel>> Login()
        //{
        //    string userId = HttpContext.User.FindFirstValue("id");
        //    string role = HttpContext.User.FindFirstValue(ClaimTypes.Role);

        //    User? user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userId));

        //    if (user != null)
        //    {
        //        if (role == "tutor")
        //        {
        //            Tutor tutor = await _uow.TutorRepo.SingleOrDefaultAsync(t => t.UserId == Guid.Parse(userId));

        //            return new EditProfileModel() { Email = user.Email, FirstName = tutor.FirstName, LastName = tutor.LastName, PhoneNumber = tutor.PhoneNumber.ToString() };
        //        }
        //        else if (role == "student")
        //        {
        //            Student student = await _uow.StudentRepo.SingleOrDefaultAsync(s => s.UserId == Guid.Parse(userId));

        //            return new EditProfileModel() { Email = user.Email, FirstName = student.FirstName, LastName = student.LastName, PhoneNumber = student.PhoneNumber.ToString() };
        //        }
        //    }

        //    return BadRequest(new RequestResult { Successful = false, Error = "User id is invalid" });
        //}

        [HttpPost("Create")]
        [Authorize(Roles = "admin, tutor")]
        public async Task<ActionResult<RequestResult>> Create([FromBody] MaterialGroupViewModel materialGroupViewModel)
        {
            if (materialGroupViewModel == null)
            {
                return BadRequest("Nazwa grupy jest pusta");
            }
           
            MaterialsGroup materialsGroup = new MaterialsGroup() { Name = materialGroupViewModel.Name};
            await _uow.MaterialsGroupRepo.AddAsync(materialsGroup);
            await _uow.CompleteAsync();

            return Ok(new RequestResult { Successful = true, Message = "Utworzono grupe materiałów o nazwie " + materialGroupViewModel.Name });
        }
    }
}
