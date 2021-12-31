using Models.Models;
using Models.ViewModels;

namespace MyTutoring.Client.Services.Activities
{
    public interface IActivitiesService
    {
        Task<RequestResult> CreateActivity(ActivitySingleViewModel activity);
        Task<RequestResult> DeleteActivity(int Id);
        Task<RequestResult> EditActivity(ActivitySingleViewModel activity);
        Task<IEnumerable<ActivitySingleViewModel>> GetActivitiesList(UserInfo userInfo);
    }
}