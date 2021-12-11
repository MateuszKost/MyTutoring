using Models;

namespace MyTutoring.Client.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<HttpResponseMessage> Register(RegisterModel model);
        Task<RequestResult> Login(LoginModel loginModel);
        Task Logout();
    }
}