using DataAccessLayer;
using DataEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;
using MyTutoring.BlobStorageManager.Containers;
using MyTutoring.BlobStorageManager.Context;
using Services.FileConverter;

namespace MyTutoring.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeworkController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IStorageContext<IStorageContainer> _storageContext;

        public HomeworkController(IStorageContext<IStorageContainer> storageContext)
        {
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
            _storageContext = storageContext;
        }

        [HttpPost("Create")]
        [Authorize(Roles = "admin, tutor")]
        public async Task<ActionResult<RequestResult>> Create([FromBody] HomeworkSingleViewModel homeworkModel)
        {
            if (homeworkModel == null || homeworkModel.DataTask == null)
            {
                return BadRequest(new RequestResult { Successful = false, Message = "Nie podano wszystkich danych" });
            }

            Homework baseHomework = await _uow.HomeworkRepo.SingleOrDefaultAsync(m => m.Name == homeworkModel.FileName);
            if (baseHomework != null)
            {
                return BadRequest(new RequestResult { Successful = false, Message = "Plik o takiej nazwie już istnieje" });
            }

            Student student = await _uow.StudentRepo.SingleOrDefaultAsync(s => s.UserId == Guid.Parse(homeworkModel.StudentId));
            Tutor tutor = await _uow.TutorRepo.SingleOrDefaultAsync(s => s.UserId == Guid.Parse(homeworkModel.TutorId));

            Homework homework = new Homework()
            {
                Name = homeworkModel.Name,
                StudentId = Guid.Parse(homeworkModel.StudentId),
                TutorId = Guid.Parse(homeworkModel.TutorId),
                StartTime = DateTime.Now,
                EndTime = homeworkModel.EndTime,
                Status = false,
                Grade = 0.0F,
                Student = student,
                Tutor = tutor
            };
            await _uow.HomeworkRepo.AddAsync(homework);
            await _uow.CompleteAsync();

            Homework createdHomework = await _uow.HomeworkRepo.SingleOrDefaultAsync(h => h.Name == homeworkModel.Name);

            byte[] file = FileConverter.Base64ToFile(homeworkModel.DataTask);

            Material material = new Material()
            {
                Name = homeworkModel.Name,
                Description = "materiał z zadanianiami",
                MaterialGroupId = null,
                MaterialTypeId = null,
                HomeworkId = createdHomework.Id,
                FileName = homeworkModel.FileName
            };
            await _uow.MaterialRepo.AddAsync(material);
            await _uow.CompleteAsync();

            _storageContext.AddAsync(new TaskContainer(), file, material.FileName);

            return Ok(new RequestResult { Successful = true, Message = "Materiał o nazwie " + homeworkModel.FileName });
        }

        [HttpPost("Getall")]
        [Authorize(Roles = "admin, tutor, student")]
        public async Task<ActionResult<HomeworkViewModel>> GetAll([FromBody] HomeworkRequest homeworkRequest)
        {
            ICollection<HomeworkSingleViewModel> homeworkSingleViewModels = new List<HomeworkSingleViewModel>();
            IEnumerable<Homework> homeworks = new List<Homework>();

            if (homeworkRequest == null)
            {
                return new HomeworkViewModel { Homeworks = null };
            }

            switch (homeworkRequest.UserRole)
            {
                case "tutor":
                    homeworks = await _uow.HomeworkRepo.WhereAsync(h => h.TutorId == Guid.Parse(homeworkRequest.UserId) && h.Status == homeworkRequest.Status);
                    break;
                case "student":
                    homeworks = await _uow.HomeworkRepo.WhereAsync(h => h.StudentId == Guid.Parse(homeworkRequest.UserId) && h.Status == homeworkRequest.Status);
                    break;
                case "admin":
                    homeworks = await _uow.HomeworkRepo.WhereAsync(h => h.Status == homeworkRequest.Status);
                    break;
            }

            foreach (var homework in homeworks)
            {
                homeworkSingleViewModels.Add(new HomeworkSingleViewModel
                {
                    FileName = "",
                    Name = homework.Name,
                    StudentId = homework.StudentId.ToString(),
                    TutorId = homework.TutorId.ToString(),
                    EndTime = homework.EndTime,
                    DataTask = "",
                    Grade = homework.Grade,
                    Status = homework.Status,
                    Id = homework.Id
                });
            }

            return new HomeworkViewModel { Homeworks = homeworkSingleViewModels };
        }

        [HttpPost("Get")]
        [Authorize(Roles = "admin, tutor, student")]
        public async Task<ActionResult<HomeworkSingleViewModel>> Get([FromBody] SingleHomeworkRequest homeworkRequest)
        {
            if (homeworkRequest == null)
            {
                return null;
            }

            Homework homework = await _uow.HomeworkRepo.SingleOrDefaultAsync(h => h.Id == homeworkRequest.HomeworkId);

            if (homework == null)
            {
                return null;
            }

            Material material = await _uow.MaterialRepo.SingleOrDefaultAsync(h => h.HomeworkId == homeworkRequest.HomeworkId);

            if (material == null)
            {
                return null;
            }

            TaskSolution taskSolution = await _uow.TaskSolutionRepo.SingleOrDefaultAsync(h => h.HomeworkId == homeworkRequest.HomeworkId);

            if (taskSolution != null)
            {
                Uri urlTask = await _storageContext.GetAsync(new TaskContainer(), material.FileName);
                Uri urlSolution = await _storageContext.GetAsync(new TaskSolutionContainer(), material.FileName);

                HomeworkSingleViewModel homeworkSingleViewModel = new HomeworkSingleViewModel
                {
                    FileName = material.Name,
                    Name = homework.Name,
                    StudentId = homework.StudentId.ToString(),
                    TutorId = homework.TutorId.ToString(),
                    EndTime = homework.EndTime,
                    TaskSolutionFileName = taskSolution.FileName,
                    UrlTask = urlTask,
                    UrlTaskSolution = urlSolution,
                    Grade = homework.Grade,
                    Status = homework.Status,
                    Id = homework.Id
                };
                return homeworkSingleViewModel;
            }
            else
            {
                Uri urlTask = await _storageContext.GetAsync(new TaskContainer(), material.FileName);

                HomeworkSingleViewModel homeworkSingleViewModel = new HomeworkSingleViewModel
                {
                    FileName = material.Name,
                    Name = homework.Name,
                    StudentId = homework.StudentId.ToString(),
                    TutorId = homework.TutorId.ToString(),
                    EndTime = homework.EndTime,
                    UrlTask = urlTask,
                    Grade = homework.Grade,
                    Status = homework.Status,
                    Id = homework.Id
                };

                return homeworkSingleViewModel;
            }
        }

        [HttpPost("GetToChangeGrade")]
        [Authorize(Roles = "admin, tutor")]
        public async Task<ActionResult<ChangeGradeViewModel>> GetToChangeGrade([FromBody] SingleHomeworkRequest homeworkRequest)
        {
            if (homeworkRequest == null)
            {
                return null;
            }

            Homework homework = await _uow.HomeworkRepo.SingleOrDefaultAsync(h => h.Id == homeworkRequest.HomeworkId);

            if (homework == null)
            {
                return null;
            }

            return new ChangeGradeViewModel { Id = homework.Id, Name = homework.Name, Grade = homework.Grade, Status = homework.Status };
        }

        [HttpPost("ChangeGrade")]
        [Authorize(Roles = "tutor, admin")]
        public async Task<ActionResult<RequestResult>> ChangeGrade([FromBody] ChangeGradeViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            Homework homework = await _uow.HomeworkRepo.SingleOrDefaultAsync(h => h.Id == model.Id);
            homework.Grade = model.Grade;
            homework.Status = model.Status;
            _uow.HomeworkRepo.Update(homework);
            await _uow.CompleteAsync();

            return Ok(new RequestResult { Successful = true, Message = "Poprawnie zmieniono ocenę." });
        }

        [HttpPost("AddTaskSolution")]
        [Authorize(Roles = "student, admin")]
        public async Task<ActionResult<RequestResult>> AddTaskSolution([FromBody] SolutionViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            byte[] file = FileConverter.Base64ToFile(model.DataTaskSolution);

            TaskSolution taskSolution = new TaskSolution
            {
                FileName = model.TaskSolutionFileName,
                HomeworkId = model.HomeworkId
            };
            await _uow.TaskSolutionRepo.AddAsync(taskSolution);
            await _uow.CompleteAsync();

            _storageContext.AddAsync(new TaskSolutionContainer(), file, taskSolution.FileName);

            return Ok(new RequestResult { Successful = true, Message = "Materiał o nazwie " + taskSolution.FileName });
        }
    }
}
