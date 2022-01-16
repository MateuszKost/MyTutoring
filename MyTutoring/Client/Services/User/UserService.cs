using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models.Models;
using Models.ViewModels;
using MyTutoring.Client.Services.Refresh;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MyTutoring.Client.Services.EditProfile
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IRefreshService _refreshService;

        public UserService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _refreshService = ClientFactory.CreateRefreshService(httpClient, authenticationStateProvider, localStorage);
        }

        public async Task<IEnumerable<StudentSingleViewModel>> GetStudents()
        {
            await _refreshService.Refresh();
            StudentViewModel studentViewModel = await _httpClient.GetFromJsonAsync<StudentViewModel>("User/Getallstudents");

            return studentViewModel.Students;
        }

        public async Task<IEnumerable<StudentSingleViewModel>> GetTutors()
        {
            await _refreshService.Refresh();
            StudentViewModel studentViewModel = await _httpClient.GetFromJsonAsync<StudentViewModel>("User/Getalltutors");

            return studentViewModel.Students;
        }

        public async Task<EditProfileViewModel?> GetEditProfileModel()
        {
            await _refreshService.Refresh();

            return await _httpClient.GetFromJsonAsync<EditProfileViewModel>("User/Get");
        }

        public async Task<RequestResult> EditProfile(EditProfileViewModel model)
        {
            await _refreshService.Refresh();

            var response = await _httpClient.PostAsync("User/Edit", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<RequestResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<RequestResult> DeleteStudent(UserInfo model)
        {
            await _refreshService.Refresh();
            var response = await _httpClient.PostAsync("User/DeleteStudent", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<RequestResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<RequestResult> DeleteTutor(UserInfo model)
        {
            await _refreshService.Refresh();
            var response = await _httpClient.PostAsync("User/DeleteTutor", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<RequestResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
