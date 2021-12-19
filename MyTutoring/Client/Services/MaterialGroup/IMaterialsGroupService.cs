using Models.Models;
using Models.ViewModels;

namespace MyTutoring.Client.Services.MaterialGroup
{
    public interface IMaterialsGroupService
    {
        Task<RequestResult> CreateMaterialsGroup(MaterialGroupSingleViewModel model);
        Task<RequestResult> DeleteMaterialsGroup(MaterialGroupSingleViewModel model);
        Task<RequestResult> EditMaterialsGroup(MaterialGroupSingleViewModel model);
        Task<IEnumerable<MaterialGroupSingleViewModel>> GetMaterialGroupList(UserInfoModel userInfoModel);
    }
}