using Models;

namespace MyTutoring.Client.Services
{
    public interface IEditProfileService
    {
        Task<RequestResult> EditProfile(EditProfileModel model);
        Task<EditProfileModel?> GetEditProfileModel();
    }
}