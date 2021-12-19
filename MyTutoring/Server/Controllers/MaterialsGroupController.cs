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
    public class MaterialsGroupController : Controller
    {
        private readonly IUnitOfWork _uow;

        public MaterialsGroupController()
        {
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
        }

        [HttpPost("Create")]
        [Authorize(Roles = "admin, tutor")]
        public async Task<ActionResult<RequestResult>> Create([FromBody] MaterialGroupSingleViewModel materialGroupViewModel)
        {
            if (materialGroupViewModel == null)
            {
                return BadRequest(new RequestResult { Successful = false, Message = "Nie podano nazwy dla grupy materiału"});
            }
            var materialGroup = await _uow.MaterialsGroupRepo.SingleOrDefaultAsync(x => x.Name == materialGroupViewModel.Name);
            if (materialGroup != null)
            {
                return BadRequest(new RequestResult { Successful = false, Message = "Grupa o takiej nazwie już istnieje" });
            }

            MaterialsGroup materialsGroup = new MaterialsGroup() { Name = materialGroupViewModel.Name };
            await _uow.MaterialsGroupRepo.AddAsync(materialsGroup);
            await _uow.CompleteAsync();

            return Ok(new RequestResult { Successful = true, Message = "Utworzono grupe materiałów o nazwie " + materialGroupViewModel.Name });
        }

        [HttpPost("Getall")]
        [Authorize(Roles = "admin, tutor, student")]
        public async Task<ActionResult<MaterialsGroupViewModel>> GetAll([FromBody] UserInfo model)
        {
            IEnumerable<MaterialGroupSingleViewModel> materialGroupViewModels = new List<MaterialGroupSingleViewModel>();
            if (model == null)
            {
                return new MaterialsGroupViewModel { MaterialGroupSingleViewModels = null};
            }

            if (model.Role == "admin" || model.Role == "tutor")
            {
                var list = await _uow.MaterialsGroupRepo.GetAllAsync();
                materialGroupViewModels = list.Select(x => new MaterialGroupSingleViewModel { Name = x.Name, MaterialGroupId = x.Id });
            }
            else
            {
                var list = await _uow.MaterialsGroupVisibilityRepo.WhereAsync(x => x.StudentId == Guid.Parse(model.Id) && x.IsVisible == true);
                materialGroupViewModels = list.Select(x => new MaterialGroupSingleViewModel { Name = x.MaterialsGroup.Name, MaterialGroupId = x.MaterialsGroup.Id });
            }

            return new MaterialsGroupViewModel { MaterialGroupSingleViewModels = materialGroupViewModels };
        }
    }
}
