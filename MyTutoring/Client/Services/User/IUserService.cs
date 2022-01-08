using Models.Models;
using Models.ViewModels;

namespace MyTutoring.Client.Services.EditProfile
{
    public interface IUserService
    {
        Task<IEnumerable<StudentSingleViewModel>> GetStudents();
        Task<IEnumerable<StudentSingleViewModel>> GetTutors();
        Task<RequestResult> EditProfile(EditProfileViewModel model);
        Task<EditProfileViewModel?> GetEditProfileModel();
        Task<RequestResult> DeleteStudent(UserInfo model);
        Task<RequestResult> DeleteTutor(UserInfo model);
    }
}