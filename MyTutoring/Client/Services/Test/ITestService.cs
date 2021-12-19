using Models.Models;

namespace MyTutoring.Client.Services.Test
{
    public interface ITestService
    {
        Task<UserInfoModel> GetModel();
    }
}