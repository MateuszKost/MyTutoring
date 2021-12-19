using DataAccessLayer;
using DataEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;
using MyTutoring.BlobStorageManager.Containers;
using MyTutoring.BlobStorageManager.Context;
using Services.FileConverter;
using Services.HashGenerator;

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

            Material baseMaterial = await _uow.MaterialRepo.SingleOrDefaultAsync(m => m.Name == materialModel.Name);
            if (baseMaterial != null)
            {
                return BadRequest(new RequestResult { Successful = false, Message = "Plik o takiej nazwie już istnieje" });
            }

            byte[] imageFile = FileConverter.Base64ToImage(materialModel.Data);

            Material material = new Material() { Name = materialModel.Name, Description = materialModel.Description, MaterialGroupId = materialModel.MaterialGroupId, MaterialTypeId = materialModel.MaterialTypeId,
                FileSha1 = Hashgenerator.GetHash(imageFile)};
            await _uow.MaterialRepo.AddAsync(material);
            await _uow.CompleteAsync();

            _storageContext.AddAsync(new FileContainer(), imageFile, material.FileSha1);

            return Ok(new RequestResult { Successful = true, Message = "Materiał o nazwie " + materialModel.Name });
        }

        //[HttpPost("Getall")]
        //[Authorize(Roles = "admin, tutor, student")]
        //public async Task<ActionResult<MaterialsGroupViewModel>> GetAll([FromBody] UserInfo model)
        //{
        //    IEnumerable<MaterialGroupSingleViewModel> materialGroupViewModels = new List<MaterialGroupSingleViewModel>();
        //    if (model == null)
        //    {
        //        return new MaterialsGroupViewModel { MaterialGroupSingleViewModels = null };
        //    }

        //    if (model.Role == "admin" || model.Role == "tutor")
        //    {
        //        var list = await _uow.MaterialsGroupRepo.GetAllAsync();
        //        materialGroupViewModels = list.Select(x => new MaterialGroupSingleViewModel { Name = x.Name, MaterialGroupId = x.Id });
        //    }
        //    else
        //    {
        //        var list = await _uow.MaterialsGroupVisibilityRepo.WhereAsync(x => x.StudentId == Guid.Parse(model.Id) && x.IsVisible == true);
        //        materialGroupViewModels = list.Select(x => new MaterialGroupSingleViewModel { Name = x.MaterialsGroup.Name, MaterialGroupId = x.MaterialsGroup.Id });
        //    }

        //    return new MaterialsGroupViewModel { MaterialGroupSingleViewModels = materialGroupViewModels };
        //}
    }
}
