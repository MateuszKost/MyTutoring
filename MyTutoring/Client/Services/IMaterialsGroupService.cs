using Models;

namespace MyTutoring.Client.Services
{
    public interface IMaterialsGroupService
    {
        Task<RequestResult> CreateMaterialsGroup(MaterialGroupViewModel model);
        Task<RequestResult> DeleteMaterialsGroup(MaterialGroupViewModel model);
        Task<RequestResult> EditMaterialsGroup(MaterialGroupViewModel model);
    }
}