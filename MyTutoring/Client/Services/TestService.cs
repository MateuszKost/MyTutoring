using Models;
using System.Net.Http.Json;

namespace MyTutoring.Client.Services
{
    public class TestService : ITestService
    {
        private readonly HttpClient _httpClient;
        private readonly string api = "home";

        public TestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TestModel> GetModel()
        {
            var result = await _httpClient.GetFromJsonAsync<TestModel>("home/test");
            return result;
        }
    }
}
