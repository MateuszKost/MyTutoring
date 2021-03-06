using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models.Models;
using Models.ViewModels;
using MyTutoring.Client.Services.Refresh;
using System.Text;
using System.Text.Json;

namespace MyTutoring.Client.Services.Material
{
    public class MaterialService : IMaterialService
    {
        private readonly HttpClient _httpClient;
        private readonly IRefreshService _refreshService;

        public MaterialService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _refreshService = ClientFactory.CreateRefreshService(httpClient, authenticationStateProvider, localStorage);
        }

        public async Task<MaterialsViewModel> GetMaterialViewModelList(int materialGroupId)
        {
            await _refreshService.Refresh();
            var model = new MaterialGroupSingleViewModel { MaterialGroupId = materialGroupId, Name = "connect" };

            var response = await _httpClient.PostAsync("Material/Getall", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));
            MaterialsViewModel materialsGroupViewModel = JsonSerializer.Deserialize<MaterialsViewModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return materialsGroupViewModel;
        }

        public async Task<RequestResult> CreateMaterial(MaterialViewModel model)
        {
            await _refreshService.Refresh();

            var response = await _httpClient.PostAsync("Material/Create", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<RequestResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<RequestResult> EditMaterial(MaterialViewModel model)
        {
            await _refreshService.Refresh();

            var response = await _httpClient.PostAsync("Material/Edit", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<RequestResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<RequestResult> DeleteMaterial(MaterialViewModel model)
        {
            await _refreshService.Refresh();
            var response = await _httpClient.PostAsync("Material/Delete", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<RequestResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<MaterialViewModel> GetMaterial(SingleItemByIdRequest model)
        {
            await _refreshService.Refresh();

            var response = await _httpClient.PostAsync("Material/Get", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<MaterialViewModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
