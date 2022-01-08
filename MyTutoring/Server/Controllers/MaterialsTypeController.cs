using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;

namespace MyTutoring.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MaterialsTypeController : Controller
    {
        private readonly IUnitOfWork _uow;

        public MaterialsTypeController(IUnitOfWork unitOfWork = null)
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

        [HttpGet("Getall")]
        [Authorize(Roles = "admin, tutor, student")]
        public async Task<ActionResult<MaterialTypeViewModel>> GetAll()
        {
            IEnumerable<MaterialTypeSingleViewModel> materialTypeViewModels = new List<MaterialTypeSingleViewModel>();

            var list = await _uow.MaterialTypeRepo.GetAllAsync();
            materialTypeViewModels = list.Select(x => new MaterialTypeSingleViewModel { Name = x.Name, MaterialTypeId = x.Id });

            return new MaterialTypeViewModel { MaterialTypeSingleViewModels = materialTypeViewModels };
        }
    }
}
