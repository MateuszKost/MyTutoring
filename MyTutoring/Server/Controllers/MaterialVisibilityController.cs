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
    public class MaterialVisibilityController : Controller
    {
        private readonly IUnitOfWork _uow;

        public MaterialVisibilityController(IUnitOfWork unitOfWork = null)
        {
            if (unitOfWork == null)
            {
                _uow = DataAccessLayerFactory.CreateUnitOfWork();
            }
            else
            {
                _uow = unitOfWork;
            }
        }        

        [HttpPost("Getvisibilitylist")]
        [Authorize(Roles = "admin, tutor")]
        public async Task<ActionResult<VisibilityViewModel>> GetAllVisibilities([FromBody] UserInfo model)
        {
            ICollection<VisibilitySingleViewModel> visibilities = new List<VisibilitySingleViewModel>();
            if (model == null)
            {
                return new VisibilityViewModel { Visibilities = null };
            }

            var materialGroupList = await _uow.MaterialsGroupRepo.GetAllAsync();

            foreach (var material in materialGroupList)
            {
                var materialGroupVisibility = await _uow.MaterialsGroupVisibilityRepo.SingleOrDefaultAsync(m => m.MaterialsGroupId == material.Id && m.StudentId == Guid.Parse(model.Id));

                if (materialGroupVisibility == null)
                {
                    visibilities.Add(new VisibilitySingleViewModel { MaterialGroupId = material.Id, MaterialGroupName = material.Name, StudentId = model.Id, IsVisible = false });
                }
                else
                {
                    visibilities.Add(new VisibilitySingleViewModel { MaterialGroupId = material.Id, MaterialGroupName = material.Name, StudentId = model.Id, IsVisible = materialGroupVisibility.IsVisible });
                }
            }

            return new VisibilityViewModel { Visibilities = visibilities };
        }

        [HttpPost("Setvisibilitylist")]
        [Authorize(Roles = "admin, tutor")]
        public async Task<ActionResult<RequestResult>> SetAllVisibilities([FromBody] VisibilityViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            Guid studentId = Guid.Parse(model.Visibilities.First().StudentId);
            MaterialsGroupVisibility materialsVisibility = null;

            foreach (var visibi in model.Visibilities)
            {
                materialsVisibility = await _uow.MaterialsGroupVisibilityRepo.SingleOrDefaultAsync(m => m.StudentId == studentId && m.MaterialsGroupId == visibi.MaterialGroupId);
                if (materialsVisibility != null)
                    break;
            }

            if (materialsVisibility == null)
            {
                IEnumerable<MaterialsGroupVisibility> materials = model.Visibilities.Select(m => new MaterialsGroupVisibility { MaterialsGroupId = m.MaterialGroupId, StudentId = Guid.Parse(m.StudentId), IsVisible = m.IsVisible });
                await _uow.MaterialsGroupVisibilityRepo.AddRangeAsync(materials);
                await _uow.CompleteAsync();
            }
            else
            {
                IEnumerable<MaterialsGroupVisibility> materialVisibilities = await _uow.MaterialsGroupVisibilityRepo.WhereAsync(m => m.StudentId == studentId);

                foreach (var material in model.Visibilities)
                {
                    if (materialVisibilities.Any(m => m.StudentId == studentId && m.MaterialsGroupId == material.MaterialGroupId))
                    {
                        var updatedMaterialVisibility = await _uow.MaterialsGroupVisibilityRepo.SingleOrDefaultAsync(m => m.StudentId == studentId && m.MaterialsGroupId == material.MaterialGroupId);
                        updatedMaterialVisibility.IsVisible = material.IsVisible;
                        _uow.MaterialsGroupVisibilityRepo.Update(updatedMaterialVisibility);
                        await _uow.CompleteAsync();
                    }
                    else
                    {
                        await _uow.MaterialsGroupVisibilityRepo.AddAsync(new MaterialsGroupVisibility { MaterialsGroupId = material.MaterialGroupId, StudentId = studentId, IsVisible = material.IsVisible });
                        await _uow.CompleteAsync();
                    }
                }
            }

            return Ok(new RequestResult { Successful = true, Message = "Dodano poprawnie." });
        }
    }
}
