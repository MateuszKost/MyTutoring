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

        public async Task<IEnumerable<MaterialViewModel>> GetMaterialViewModelList()
        {
            await _refreshService.Refresh();

            throw new NotImplementedException();
        }

        public async Task<RequestResult> CreateMaterial(MaterialViewModel model)
        {
            await _refreshService.Refresh();

            var response = await _httpClient.PostAsync("Material/Create", new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<RequestResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<RequestResult> EditMaterial(MaterialViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<RequestResult> DeleteMaterial(MaterialViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
