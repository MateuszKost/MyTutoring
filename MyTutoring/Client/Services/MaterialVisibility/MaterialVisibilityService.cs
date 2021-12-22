using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models.ViewModels;
using MyTutoring.Client.Services.Refresh;
using System.Net.Http.Json;

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

        public async Task<IEnumerable<StudentSingleViewModel>> GetStudents()
        {
            await _refreshService.Refresh();
            StudentViewModel studentViewModel = await _httpClient.GetFromJsonAsync<StudentViewModel>("MaterialVisibility/Getall");

            return studentViewModel.Students;
        }
    }
}
