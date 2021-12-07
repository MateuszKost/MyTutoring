using Models;

namespace MyTutoring.Client.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<HttpResponseMessage> Register(RegisterModel model);
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
    }
}