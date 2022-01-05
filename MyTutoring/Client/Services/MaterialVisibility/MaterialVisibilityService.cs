using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models.Models;
using Models.ViewModels;
using MyTutoring.Client.Services.Refresh;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MyTutoring.Client.Services.MaterialVisibility
{
    public class MaterialVisibilityService : IMaterialVisibilityService
    {
        private readonly HttpClient _httpClient;
        private readonly IRefreshService _refreshService;

        public MaterialVisibilityService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _refreshService = ClientFactory.CreateRefreshService(httpClient, authenticationStateProvider, localStorage);
        }        

        public async Task<ICollection<VisibilitySingleViewModel>> GetVisibilityList(string studentId)
        {
            await _refreshService.Refresh();
            var model = new UserInfo { Id = studentId, Role = "" };

            var response = await _httpClient.PostAsync("MaterialVisibility/Getvisibilitylist", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));
            VisibilityViewModel visibilityViewModel = JsonSerializer.Deserialize<VisibilityViewModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return visibilityViewModel.Visibilities;
        }

        public async Task<RequestResult> SetVisibilityList(ICollection<VisibilitySingleViewModel> Visibilities)
        {
            await _refreshService.Refresh();
            VisibilityViewModel visibilityViewModel = new VisibilityViewModel { Visibilities = Visibilities };

            var response = await _httpClient.PostAsync("MaterialVisibility/Setvisibilitylist", new StringContent(JsonSerializer.Serialize(visibilityViewModel), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<RequestResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
