using Models.Models;
using Models.ViewModels;

namespace MyTutoring.Client.Services.EditProfile
{
    public interface IEditProfileService
    {
        Task<RequestResult> EditProfile(EditProfileViewModel model);
        Task<EditProfileViewModel?> GetEditProfileModel();
    }
}