using DataAccessLayer;
using DataEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;

namespace MyTutoring.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActivityController : Controller
    {
        private readonly IUnitOfWork _uow;

        public ActivityController()
        {
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
        }

        [HttpPost("Create")]
        [Authorize(Roles = "tutor")]
        public async Task<ActionResult<RequestResult>> Create([FromBody] ActivitySingleViewModel model)
        {
            if (model == null)
            {
                return BadRequest(new RequestResult { Successful = false, Message = "Nie podano wszystkich danych" });
            }

            //Activity activity = await _uow.ActivityRepo.SingleOrDefaultAsync(m => m.Name == model.Name);
            //if (activity != null)
            //{
            //    return BadRequest(new RequestResult { Successful = false, Message = "Aktywność o takiej nazwie już istnieje" });
            //}

            Dictionary<int, string> daysOfWeek = ActivityTimeList.CreateDayOfWeek();
            int day = daysOfWeek.FirstOrDefault(d => d.Value == model.DayOfWeek).Key;
            Activity newActivity = new Activity()
            {
                Id = model.Id,
                Name = model.Name,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                StudentId = Guid.Parse(model.StudentId),
                TutorId = Guid.Parse(model.TutorId),
                DayOfWeek = day
            };
            await _uow.ActivityRepo.AddAsync(newActivity);
            await _uow.CompleteAsync();

            return Ok(new RequestResult { Successful = true, Message = "Aktywnosc o nazwie " + model.Name });
        }

        [HttpPost("Getall")]
        [Authorize(Roles = "tutor, student")]
        public async Task<ActionResult<ActivityViewModel>> GetAll([FromBody] UserInfo userInfo)
        {
            ICollection<ActivitySingleViewModel> activities = new List<ActivitySingleViewModel>();

            if (userInfo == null)
            {
                return new ActivityViewModel { Activities = null };
            }

            IEnumerable<Activity> dbActivities = new List<Activity>();
            string userName = "";           

            dbActivities = await _uow.ActivityRepo.GetAllAsync();
            Activity? dbActivityTutor = dbActivities.FirstOrDefault(a => a.TutorId == Guid.Parse(userInfo.Id));
            Activity? dbActivityStudent = dbActivities.FirstOrDefault(a => a.StudentId == Guid.Parse(userInfo.Id));
            if (dbActivityTutor == null && dbActivityStudent == null)
            {
                return new ActivityViewModel { Activities = activities };
            }

            Dictionary<int, string> daysOfWeek = ActivityTimeList.CreateDayOfWeek();

            foreach (Activity activity in dbActivities)
            {
                if (userInfo.Role == "student")
                {
                    Tutor tutor = await _uow.TutorRepo.SingleOrDefaultAsync(t => t.UserId == activity.TutorId);
                    userName = tutor.FirstName + " " + tutor.LastName;
                }
                else if (userInfo.Role == "tutor")
                {
                    Student student = await _uow.StudentRepo.SingleOrDefaultAsync(t => t.UserId == activity.StudentId);
                    userName = student.FirstName + " " + student.LastName;
                }
                activities.Add(new ActivitySingleViewModel { Id = activity.Id, Name = activity.Name, UserName = userName, StartTime = activity.StartTime, EndTime = activity.EndTime, DayOfWeek = daysOfWeek[activity.DayOfWeek] });
            }

            return new ActivityViewModel { Activities = activities };
        }
    }
}
