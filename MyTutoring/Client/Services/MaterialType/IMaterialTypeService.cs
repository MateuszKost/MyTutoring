using Models.ViewModels;

namespace MyTutoring.Client.Services.MaterialType
{
    public interface IMaterialTypeService
    {
        Task<IEnumerable<MaterialTypeSingleViewModel>> GetMaterialTypeList();
    }
}