using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models.Models;
using Models.ViewModels;
using MyTutoring.Client.Services.Refresh;
using System.Text;
using System.Text.Json;

namespace MyTutoring.Client.Services.MaterialGroup
{
    public class MaterialsGroupService : IMaterialsGroupService
    {
        private readonly HttpClient _httpClient;
        private readonly IRefreshService _refreshService;

        public MaterialsGroupService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _refreshService = ClientFactory.CreateRefreshService(httpClient, authenticationStateProvider, localStorage);
        }

        public async Task<RequestResult> CreateMaterialsGroup(MaterialGroupSingleViewModel model)
        {
            await _refreshService.Refresh();

            var response = await _httpClient.PostAsync("MaterialsGroup/Create", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<RequestResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<RequestResult> EditMaterialsGroup(MaterialGroupSingleViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<RequestResult> DeleteMaterialsGroup(MaterialGroupSingleViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MaterialGroupSingleViewModel>> GetMaterialGroupList(UserInfo model)
        {
            await _refreshService.Refresh();

            var response = await _httpClient.PostAsync("MaterialsGroup/Getall", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));
            MaterialsGroupViewModel materialsGroupViewModel = JsonSerializer.Deserialize<MaterialsGroupViewModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return materialsGroupViewModel.MaterialGroupSingleViewModels;
        }            
    }
}
