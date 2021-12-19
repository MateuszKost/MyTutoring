using Models;

namespace MyTutoring.Client.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<RequestResult> Register(RegisterModel model);
        Task<RequestResult> Login(LoginModel loginModel);
        Task Logout();
    }
}