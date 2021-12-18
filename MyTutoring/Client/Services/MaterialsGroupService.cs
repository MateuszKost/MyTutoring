using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models;
using System.Text;
using System.Text.Json;

namespace MyTutoring.Client.Services
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

        public async Task<RequestResult> CreateMaterialsGroup(MaterialGroupViewModel model)
        {
            await _refreshService.Refresh();

            var response = await _httpClient.PostAsync("MaterialsGroup/Create", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<RequestResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<RequestResult> EditMaterialsGroup(MaterialGroupViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<RequestResult> DeleteMaterialsGroup(MaterialGroupViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
