using DataAccessLayer;
using DataEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Models;
using Models.ViewModels;
using MyTutoring.Services.PasswordHasher;
using Services;
using System.Security.Claims;

namespace MyTutoring.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _uow;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            _uow = DataAccessLayerFactory.CreateUnitOfWork();        
            _passwordHasher = ServicesFactory.CreateBCryptPasswordHasher();
        }

        [HttpGet("Get")]
        [Authorize(Roles = "student, tutor")]
        public async Task<ActionResult<EditProfileViewModel>> Login()
        {
            string userId = HttpContext.User.FindFirstValue("id");
            string role = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            User? user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userId));

            if (user != null)
            {
                if(role == "tutor")
                {
                    Tutor tutor = await _uow.TutorRepo.SingleOrDefaultAsync(t => t.UserId == Guid.Parse(userId));

                    return new EditProfileViewModel() { Email = user.Email, FirstName = tutor.FirstName, LastName = tutor.LastName, PhoneNumber = tutor.PhoneNumber.ToString() };
                }
                else if(role == "student")
                {
                    Student student = await _uow.StudentRepo.SingleOrDefaultAsync(s => s.UserId == Guid.Parse(userId));

                    return new EditProfileViewModel() { Email = user.Email, FirstName = student.FirstName, LastName = student.LastName, PhoneNumber = student.PhoneNumber.ToString() };
                }
            }

            return BadRequest(new RequestResult { Successful = false, Message = "User id is invalid" });
        }

        [HttpGet("Getallstudents")]
        [Authorize(Roles = "admin, tutor")]
        public async Task<ActionResult<StudentViewModel>> GetAllStudents()
        {
            ICollection<StudentSingleViewModel> students = new List<StudentSingleViewModel>();

            var list = await _uow.StudentRepo.GetAllAsync();
            foreach (var student in list)
            {
                User user = await _uow.UserRepo.SingleOrDefaultAsync(x => x.Id == student.UserId);
                students.Add(new StudentSingleViewModel { StudentName = student.FirstName + " " + student.LastName, Email = user.Email, UserId = student.UserId.ToString() });
            }

            return new StudentViewModel { Students = students };
        }

        [HttpGet("Getalltutors")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<StudentViewModel>> GetAllTutors()
        {
            ICollection<StudentSingleViewModel> tutors = new List<StudentSingleViewModel>();

            var list = await _uow.TutorRepo.GetAllAsync();
            foreach (var student in list)
            {
                User user = await _uow.UserRepo.SingleOrDefaultAsync(x => x.Id == student.UserId);
                tutors.Add(new StudentSingleViewModel { StudentName = student.FirstName + " " + student.LastName, Email = user.Email, UserId = student.UserId.ToString() });
            }

            return new StudentViewModel { Students = tutors };
        }

        [HttpPost("Edit")]
        [Authorize(Roles = "student, tutor")]
        public async Task<ActionResult<RequestResult>> Edit([FromBody] EditProfileViewModel editProfileModel)
        {
            string userId = HttpContext.User.FindFirstValue("id");
            string role = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (editProfileModel == null)
            {
                return BadRequest("Invalid client request");
            }
            User? user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userId));

            UserIdentity userIdentity = await _uow.UserIdentityRepo.SingleOrDefaultAsync(ui => ui.UserId == user.Id);
            string salt = userIdentity.Salt;
            string pepper = _configuration.GetValue<string>("PasswordSettings:Pepper");
            string passwordHash = _passwordHasher.Hash(editProfileModel.Password + pepper, salt);

            if(user.Password != passwordHash)
            {
                return BadRequest(new RequestResult { Successful = false, Message = "Aktualne haslo nie jest poprawne" });
            }

            if (editProfileModel.NewPassword != editProfileModel.RepeatPassword)
            {
                return BadRequest(new RequestResult { Successful = false, Message = "Nowe haslo i powtorzone, nie są takie same. Czy aby na pewno wprowadziłeś/aś dobre hasło?" });
            }

            if (user != null)
            {
                user.Email = editProfileModel.Email;
                user.Password = editProfileModel.NewPassword;
                _uow.UserRepo.Update(user);
                await _uow.CompleteAsync();

                if (role == "tutor")
                {
                    Tutor tutor = new Tutor() { UserId = Guid.Parse(userId), FirstName = editProfileModel.FirstName, LastName = editProfileModel.LastName, PhoneNumber = Int32.Parse(editProfileModel.PhoneNumber)};
                    _uow.TutorRepo.Update(tutor);
                    await _uow.CompleteAsync();
                }
                else if (role == "student")
                {
                    Student student = new Student() { UserId = Guid.Parse(userId), FirstName = editProfileModel.FirstName, LastName = editProfileModel.LastName, PhoneNumber = Int32.Parse(editProfileModel.PhoneNumber) };
                    _uow.StudentRepo.Update(student);
                    await _uow.CompleteAsync();
                }
                return Ok();
            }

            return BadRequest(new RequestResult { Successful = false, Message = "Błąd systemowy." });
        }

        [Authorize(Roles = "admin")]
        [HttpPost("deleteStudent")]
        public async Task<IActionResult> DeleteStudent([FromBody] UserInfo model)
        {
            Student student = await _uow.StudentRepo.SingleOrDefaultAsync(a => a.UserId == Guid.Parse(model.Id));
            if (student == null)
            {
                return BadRequest(new RequestResult { Successful = false, Message = "Nie ma takiej aktywności, błąd po stronie serwera." });
            }

            _uow.StudentRepo.Remove(student);
            await _uow.CompleteAsync();

            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("deleteTutor")]
        public async Task<IActionResult> DeleteTutor([FromBody] UserInfo model)
        {
            Tutor tutor = await _uow.TutorRepo.SingleOrDefaultAsync(a => a.UserId == Guid.Parse(model.Id));
            if (tutor == null)
            {
                return BadRequest(new RequestResult { Successful = false, Message = "Nie ma takiej aktywności, błąd po stronie serwera." });
            }

            _uow.TutorRepo.Remove(tutor);
            await _uow.CompleteAsync();

            return Ok();
        }
    }
}
