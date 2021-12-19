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

            byte[] imageFile = FileConverter.Base64ToImage(materialModel.Data);

            Material material = new Material() { Name = materialModel.Name, Description = materialModel.Description, MaterialGroupId = materialModel.MaterialGroupId, MaterialTypeId = materialModel.MaterialTypeId,
                FileName = materialModel.FileName};
            await _uow.MaterialRepo.AddAsync(material);
            await _uow.CompleteAsync();

            _storageContext.AddAsync(new FileContainer(), imageFile, material.FileName);

            return Ok(new RequestResult { Successful = true, Message = "Materiał o nazwie " + materialModel.FileName });
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

            foreach(Material material in materials)
            {
                Uri url = await _storageContext.GetAsync(new FileContainer(), material.FileName);
                materialViewModels.Add(new MaterialViewModel {Name = material. FileName = material.FileName, Description = material.Description, MaterialGroupId = material.MaterialGroupId, MaterialTypeId = material.MaterialTypeId, Url = url });
            }

            return new MaterialsViewModel { MaterialViewModels = materialViewModels };
        }
    }
}
