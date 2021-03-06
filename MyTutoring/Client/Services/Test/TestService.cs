using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models.Models;
using MyTutoring.Client.Services.Refresh;
using System.Net.Http.Json;

namespace MyTutoring.Client.Services.Test
{
    public class TestService : ITestService
    {
        private HttpClient _httpClient;
        private IRefreshService _refreshService;

        public TestService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _refreshService = ClientFactory.CreateRefreshService(httpClient, authenticationStateProvider, localStorage);
        }

        public async Task<UserInfo> GetModel()
        {
            await _refreshService.Refresh();
            var result = await _httpClient.GetFromJsonAsync<UserInfo>("home/test");
            return result;
        }
    }
}
