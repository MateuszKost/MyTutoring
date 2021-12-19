using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models;
using MyTutoring.Client.Services.Refresh;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MyTutoring.Client.Services.EditProfile
{
    public class EditProfileService : IEditProfileService
    {
        private readonly HttpClient _httpClient;
        private readonly IRefreshService _refreshService;

        public EditProfileService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _refreshService = ClientFactory.CreateRefreshService(httpClient, authenticationStateProvider, localStorage);
        }

        public async Task<EditProfileModel?> GetEditProfileModel()
        {
            await _refreshService.Refresh();

            return await _httpClient.GetFromJsonAsync<EditProfileModel>("EditProfile/Get");
        }

        public async Task<RequestResult> EditProfile(EditProfileModel model)
        {
            await _refreshService.Refresh();

            var response = await _httpClient.PostAsync("EditProfile/Edit", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<RequestResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
