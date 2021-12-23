using Models.Models;
using Models.ViewModels;

namespace MyTutoring.Client.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<RequestResult> Register(RegisterViewModel model);
        Task<RequestResult> Login(LoginViewModel loginModel);
        Task Logout();
    }
}