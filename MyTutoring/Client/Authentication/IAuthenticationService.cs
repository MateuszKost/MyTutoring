using Models;

namespace MyTutoring.Client.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
        Task Refresh();
    }
}