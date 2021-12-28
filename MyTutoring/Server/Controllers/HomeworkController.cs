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
        public async Task<ActionResult<MaterialsViewModel>> GetAll([FromBody] MaterialGroupSingleViewModel materialGroupSingleViewModel)
        {
            ICollection<MaterialViewModel> materialViewModels = new List<MaterialViewModel>();
            IEnumerable<Material> materials = new List<Material>();

            if (materialGroupSingleViewModel == null)
            {
                return new MaterialsViewModel { MaterialViewModels = null };
            }

            materials = await _uow.MaterialRepo.WhereAsync(m => m.MaterialGroupId == materialGroupSingleViewModel.MaterialGroupId);

            foreach (Material material in materials)
            {
                Uri url = await _storageContext.GetAsync(new FileContainer(), material.FileName);
                materialViewModels.Add(new MaterialViewModel { Name = material.Name = material.FileName, Description = material.Description, MaterialGroupId = (int)material.MaterialGroupId, MaterialTypeId = (int)material.MaterialTypeId, Url = url });
            }

            return new MaterialsViewModel { MaterialViewModels = materialViewModels };
        }
    }
}
