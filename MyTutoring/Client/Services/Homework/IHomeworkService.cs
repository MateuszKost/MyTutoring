using Models.Models;
using Models.ViewModels;

namespace MyTutoring.Client.Services.Homework
{
    public interface IHomeworkService
    {
        Task<RequestResult> CreateHomework(HomeworkSingleViewModel model);
        Task<RequestResult> DeleteHomework(HomeworkSingleViewModel model);
        Task<RequestResult> EditHomework(HomeworkSingleViewModel model);
        Task<IEnumerable<HomeworkSingleViewModel>> GetHomeworkViewModelList(HomeworkRequest homeworkRequest);
    }
}