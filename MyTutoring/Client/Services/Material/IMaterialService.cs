using Models.Models;
using Models.ViewModels;

namespace MyTutoring.Client.Services.Material
{
    public interface IMaterialService
    {
        Task<RequestResult> CreateMaterial(MaterialViewModel model);
        Task<RequestResult> DeleteMaterial(MaterialViewModel model);
        Task<RequestResult> EditMaterial(MaterialViewModel model);
        Task<MaterialsViewModel> GetMaterialViewModelList(int materialGroupId);
        Task<MaterialViewModel> GetMaterial(SingleItemByIdRequest model);
    }
}