using Models;

namespace MyTutoring.Client.Services
{
    public interface ITestService
    {
        Task<UserInfoModel> GetModel();
    }
}