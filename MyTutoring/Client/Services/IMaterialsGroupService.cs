using Models;

namespace MyTutoring.Client.Services
{
    public interface IMaterialsGroupService
    {
        Task<RequestResult> CreateMaterialsGroup(MaterialGroupSingleViewModel model);
        Task<RequestResult> DeleteMaterialsGroup(MaterialGroupSingleViewModel model);
        Task<RequestResult> EditMaterialsGroup(MaterialGroupSingleViewModel model);
        Task<IEnumerable<MaterialGroupSingleViewModel>> GetMaterialGroupList(UserInfoModel userInfoModel);
    }
}