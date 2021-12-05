using Models;

namespace MyTutoring.Client.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
    }
}