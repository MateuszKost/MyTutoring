using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models.Models;
using Models.ViewModels;
using MyTutoring.Client.Services.Refresh;
using System.Text;
using System.Text.Json;

namespace MyTutoring.Client.Services.Homework
{
    public class HomeworkService : IHomeworkService
    {
        private readonly HttpClient _httpClient;
        private readonly IRefreshService _refreshService;

        public HomeworkService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _refreshService = ClientFactory.CreateRefreshService(httpClient, authenticationStateProvider, localStorage);
        }

        public async Task<HomeworkSingleViewModel> GetHomeworkSingleViewModel(int homeworkId)
        {
            await _refreshService.Refresh();

            SingleItemByIdRequest homeworkRequest = new SingleItemByIdRequest { Id = homeworkId };

            var response = await _httpClient.PostAsync("Homework/Get", new StringContent(JsonSerializer.Serialize(homeworkRequest), Encoding.UTF8, "application/json"));
            HomeworkSingleViewModel homework = JsonSerializer.Deserialize<HomeworkSingleViewModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return homework;
        }

        public async Task<IEnumerable<HomeworkSingleViewModel>> GetHomeworkViewModelList(HomeworkRequest homeworkRequest)
        {
            await _refreshService.Refresh();

            var response = await _httpClient.PostAsync("Homework/Getall", new StringContent(JsonSerializer.Serialize(homeworkRequest), Encoding.UTF8, "application/json"));
            HomeworkViewModel homeworkViewModel = JsonSerializer.Deserialize<HomeworkViewModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return homeworkViewModel.Homeworks;
        }

        public async Task<RequestResult> CreateHomework(HomeworkSingleViewModel model)
        {
            await _refreshService.Refresh();

            var response = await _httpClient.PostAsync("Homework/Create", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<RequestResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<ChangeGradeViewModel> GetHomeworkToChangeGrade(int homeworkId)
        {
            await _refreshService.Refresh();

            SingleItemByIdRequest homeworkRequest = new SingleItemByIdRequest { Id = homeworkId };

            var response = await _httpClient.PostAsync("Homework/GetToChangeGrade", new StringContent(JsonSerializer.Serialize(homeworkRequest), Encoding.UTF8, "application/json"));
            ChangeGradeViewModel changeGrade = JsonSerializer.Deserialize<ChangeGradeViewModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return changeGrade;
        }

        public async Task<RequestResult> ChangeGrade(ChangeGradeViewModel model)
        {
            await _refreshService.Refresh();

            var response = await _httpClient.PostAsync("Homework/ChangeGrade", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<RequestResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<RequestResult> AddTaskSolution(SolutionViewModel model)
        {
            await _refreshService.Refresh();

            var response = await _httpClient.PostAsync("Homework/AddTaskSolution", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<RequestResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<RequestResult> EditHomework(HomeworkSingleViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<RequestResult> DeleteHomework(HomeworkSingleViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
