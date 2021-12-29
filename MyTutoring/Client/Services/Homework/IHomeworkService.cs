using Models.Models;
using Models.ViewModels;

namespace MyTutoring.Client.Services.Homework
{
    public interface IHomeworkService
    {
        Task<HomeworkSingleViewModel> GetHomeworkSingleViewModel(int homeworkId);
        Task<RequestResult> CreateHomework(HomeworkSingleViewModel model);
        Task<RequestResult> DeleteHomework(HomeworkSingleViewModel model);
        Task<ChangeGradeViewModel> GetHomeworkToChangeGrade(int homeworkId);
        Task<RequestResult> ChangeGrade(ChangeGradeViewModel model);
        Task<RequestResult> AddTaskSolution(SolutionViewModel model);
        Task<RequestResult> EditHomework(HomeworkSingleViewModel model);
        Task<IEnumerable<HomeworkSingleViewModel>> GetHomeworkViewModelList(HomeworkRequest homeworkRequest);
    }
}