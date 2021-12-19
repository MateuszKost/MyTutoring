using Models;

namespace MyTutoring.Client.Services.EditProfile
{
    public interface IEditProfileService
    {
        Task<RequestResult> EditProfile(EditProfileModel model);
        Task<EditProfileModel?> GetEditProfileModel();
    }
}