using Models.Models;

namespace MyTutoring.Client.Services.Test
{
    public interface ITestService
    {
        Task<UserInfo> GetModel();
    }
}