using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models;
using MyTutoring.Client.Services.Authentication;
using System.Text;
using System.Text.Json;

#nullable disable

namespace MyTutoring.Client.Services
{
    public class RefreshService : IRefreshService
    {
        private HttpClient _httpClient;
        private AuthenticationStateProvider _authenticationStateProvider;
        private ILocalStorageService _localStorage;

        public RefreshService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task Refresh()
        {
            string authToken = await _localStorage.GetItemAsStringAsync("authToken");
            authToken = authToken.Replace("\"", "");
            RefreshRequest refreshRequest = new RefreshRequest() { AccessToken = authToken };
            string refreshRequestAsJason = JsonSerializer.Serialize(refreshRequest);

            var response = await _httpClient.PostAsync("Authentication/Refresh", new StringContent(refreshRequestAsJason, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                await _localStorage.RemoveItemAsync("authToken");
                ((MyTutoringAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
            else
            {
                RequestResult result = JsonSerializer.Deserialize<RequestResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                await _localStorage.RemoveItemAsync("authToken");
                await _localStorage.SetItemAsync("authToken", result.AccessToken);
            }
        }
    }
}
