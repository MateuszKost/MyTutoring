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
            if (homeworkModel == null || homeworkModel.Data == null)
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

            byte[] file = FileConverter.Base64ToImage(homeworkModel.Data);

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

            if(homeworkRequest == null)
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
                    Data = "",
                    Grade = homework.Grade,
                    Status = homework.Status,
                    Id = homework.Id
                });
            }

            return new HomeworkViewModel { Homeworks = homeworkSingleViewModels };
        }
    }
}
