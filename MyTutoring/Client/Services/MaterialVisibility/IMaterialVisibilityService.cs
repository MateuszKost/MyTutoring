using Models.Models;
using Models.ViewModels;

namespace MyTutoring.Client.Services.MaterialVisibility
{
    public interface IMaterialVisibilityService
    {
        Task<ICollection<VisibilitySingleViewModel>> GetVisibilityList(string studentId);
        Task<RequestResult> SetVisibilityList(ICollection<VisibilitySingleViewModel> Visibilities);
    }
}