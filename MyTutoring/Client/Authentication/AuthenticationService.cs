using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MyTutoring.Client.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var response = await _httpClient.PostAsync("Authentication/Login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorage.SetItemAsync("authToken", loginResult.AccessToken);
            ((MyTutoringAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.AccessToken);

            return loginResult;
        }

        public async Task Logout()
        {
            var result = await _httpClient.DeleteAsync("Authentication/Logout");
            if (result.IsSuccessStatusCode)
            {
                await _localStorage.RemoveItemAsync("authToken");
                ((MyTutoringAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }
    }
}
