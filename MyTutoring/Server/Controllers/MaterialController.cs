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
    public class MaterialController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IStorageContext<IStorageContainer> _storageContext;

        public MaterialController(IStorageContext<IStorageContainer> storageContext)
        {
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
            _storageContext = storageContext;
        }

        [HttpPost("Create")]
        [Authorize(Roles = "admin, tutor")]
        public async Task<ActionResult<RequestResult>> Create([FromBody] MaterialViewModel materialModel)
        {            
            if (materialModel == null || materialModel.Data == null)
            {
                return BadRequest(new RequestResult { Successful = false, Message = "Nie podano wszystkich danych" });
            }

            Material baseMaterial = await _uow.MaterialRepo.SingleOrDefaultAsync(m => m.Name == materialModel.FileName);
            if (baseMaterial != null)
            {
                return BadRequest(new RequestResult { Successful = false, Message = "Plik o takiej nazwie już istnieje" });
            }

            byte[] file = FileConverter.Base64ToFile(materialModel.Data);

            Material material = new Material() { Name = materialModel.Name, Description = materialModel.Description, MaterialGroupId = materialModel.MaterialGroupId, MaterialTypeId = materialModel.MaterialTypeId,
                FileName = materialModel.FileName, HomeworkId = null};
            await _uow.MaterialRepo.AddAsync(material);
            await _uow.CompleteAsync();

            _storageContext.AddAsync(new FileContainer(), file, material.FileName);

            return Ok(new RequestResult { Successful = true, Message = "Materiał o nazwie " + materialModel.FileName });
        }

        [HttpPost("Edit")]
        [Authorize(Roles = "admin, tutor")]
        public async Task<ActionResult<RequestResult>> Edit([FromBody] MaterialViewModel model)
        {
            if (model == null)
            {
                return BadRequest(new RequestResult { Successful = false, Message = "Nie podano wszystkich danych" });
            }

            Material material = await _uow.MaterialRepo.SingleOrDefaultAsync(a => a.Id == model.Id);
            if (material == null)
            {
                return BadRequest(new RequestResult { Successful = false, Message = "Nie ma takiej aktywności, błąd po stronie serwera." });
            }

            material.Name = model.Name;
            material.Description = model.Name;
            material.FileName = material.FileName;
            material.MaterialTypeId = model.MaterialTypeId;
            material.MaterialGroupId = model.MaterialGroupId;

            _uow.MaterialRepo.Update(material);
            await _uow.CompleteAsync();

            return Ok(new RequestResult { Successful = true, Message = "Aktywnosc o nazwie " + model.Name + " została zmieniona." });
        }

        [HttpPost("Get")]
        [Authorize(Roles = "admin, tutor")]
        public async Task<ActionResult<MaterialViewModel>> Get([FromBody] SingleItemByIdRequest model)
        {
            if (model == null)
            {
                return null;
            }

            Material material = await _uow.MaterialRepo.SingleOrDefaultAsync(m => m.Id == model.Id);

            Uri url = await _storageContext.GetAsync(new FileContainer(), material.FileName);
            return new MaterialViewModel { Id = material.Id, Name = material.Name, Description = material.Description, MaterialGroupId = (int)material.MaterialGroupId, MaterialTypeId = (int)material.MaterialTypeId, Url = url };
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
            MaterialsGroup materialsGroup = await _uow.MaterialsGroupRepo.SingleOrDefaultAsync(m => m.Id == materialGroupSingleViewModel.MaterialGroupId);

            foreach(Material material in materials)
            {
                Uri url = await _storageContext.GetAsync(new FileContainer(), material.FileName);
                materialViewModels.Add(new MaterialViewModel {Id = material.Id, Name = material.Name, Description = material.Description, MaterialGroupId = (int)material.MaterialGroupId, MaterialTypeId = (int)material.MaterialTypeId, Url = url});
            }

            return new MaterialsViewModel { MaterialViewModels = materialViewModels, MaterialGroupName = materialsGroup.Name };
        }

        [Authorize(Roles = "admin")]
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] SingleItemByIdRequest model)
        {
            Material material = await _uow.MaterialRepo.SingleOrDefaultAsync(a => a.Id == model.Id);
            if (material == null)
            {
                return BadRequest(new RequestResult { Successful = false, Message = "Nie ma takiej aktywności, błąd po stronie serwera." });
            }

            _uow.MaterialRepo.Remove(material);
            await _uow.CompleteAsync();

            return Ok();
        }
    }
}
