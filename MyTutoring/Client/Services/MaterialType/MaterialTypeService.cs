using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models.ViewModels;
using MyTutoring.Client.Services.Refresh;
using System.Net.Http.Json;
using System.Text.Json;

namespace MyTutoring.Client.Services.MaterialType
{
    public class MaterialTypeService : IMaterialTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly IRefreshService _refreshService;

        public MaterialTypeService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _refreshService = ClientFactory.CreateRefreshService(httpClient, authenticationStateProvider, localStorage);
        }

        public async Task<IEnumerable<MaterialTypeSingleViewModel>> GetMaterialTypeList()
        {
            await _refreshService.Refresh();
            MaterialTypeViewModel materialsTypeViewModel = await _httpClient.GetFromJsonAsync<MaterialTypeViewModel>("MaterialsType/Getall");

            return materialsTypeViewModel.MaterialTypeSingleViewModels;
        }
    }
}
