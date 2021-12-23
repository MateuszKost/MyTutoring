using Models;
using Models.ViewModels;

namespace MyTutoring.Client.Pages
{
    public partial class Login
    {
        private LoginViewModel loginModel = new LoginViewModel();
        private bool ShowErrors;
        private string Error = "";

        private async Task HandleLogin()
        {
            ShowErrors = false;

            var result = await AuthService.Login(loginModel);

            if (result.Successful)
            {
                ShowErrors = false;
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Error = result.Message;
                ShowErrors = true;
            }
        }
    }
}
