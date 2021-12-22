using DataAccessLayer;
using DataEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;

namespace MyTutoring.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MaterialVisibilityController : Controller
    {
        private readonly IUnitOfWork _uow;

        public MaterialVisibilityController()
        {
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
        }

        [HttpGet("Getall")]
        [Authorize(Roles = "admin, tutor, student")]
        public async Task<ActionResult<StudentViewModel>> GetAll()
        {
            ICollection<StudentSingleViewModel> students = new List<StudentSingleViewModel>();

            var list = await _uow.StudentRepo.GetAllAsync();
            foreach(var student in list)
            {
                User user = await _uow.UserRepo.SingleOrDefaultAsync(x => x.Id == student.UserId);
                students.Add(new StudentSingleViewModel { StudentName = student.FirstName + " " + student.LastName, Email = user.Email});
            }

            return new StudentViewModel { Students = students };
        }
    }
}
