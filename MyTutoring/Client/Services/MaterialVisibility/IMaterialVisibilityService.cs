using Models.ViewModels;

namespace MyTutoring.Client.Services.MaterialVisibility
{
    public interface IMaterialVisibilityService
    {
        Task<IEnumerable<StudentSingleViewModel>> GetStudents();
    }
}