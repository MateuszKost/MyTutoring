using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models.Models;
using Models.ViewModels;
using MyTutoring.Client.Services.Refresh;
using System.Text;
using System.Text.Json;

namespace MyTutoring.Client.Services.Activities
{
    public class ActivitiesService : IActivitiesService
    {
        private readonly HttpClient _httpClient;
        private readonly IRefreshService _refreshService;

        public ActivitiesService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _refreshService = ClientFactory.CreateRefreshService(httpClient, authenticationStateProvider, localStorage);
        }

        public async Task<IEnumerable<ActivitySingleViewModel>> GetActivitiesList(UserInfo userInfo)
        {
            await _refreshService.Refresh();

            var response = await _httpClient.PostAsync("Activity/Getall", new StringContent(JsonSerializer.Serialize(userInfo), Encoding.UTF8, "application/json"));
            ActivityViewModel activityViewModel = JsonSerializer.Deserialize<ActivityViewModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return activityViewModel.Activities;
        }

        public async Task<RequestResult> CreateActivity(ActivitySingleViewModel activity)
        {
            await _refreshService.Refresh();

            var response = await _httpClient.PostAsync("Activity/Create", new StringContent(JsonSerializer.Serialize(activity), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<RequestResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<RequestResult> EditActivity(ActivitySingleViewModel activity)
        {
            throw new NotImplementedException();
        }

        public async Task<RequestResult> DeleteActivity(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
