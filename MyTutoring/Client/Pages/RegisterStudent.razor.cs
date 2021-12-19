using Models.ViewModels;

namespace MyTutoring.Client.Pages
{
    public partial class RegisterStudent
    {
        public readonly RegisterViewModel model = new RegisterViewModel();

        public bool ShowErrors { get; set; }

        public bool Success { get; set; } = false;

        string Error { get; set; }

        public async Task OnSubmit()
        {
            model.AccountType = "student";
            var result = await AuthService.Register(model);

            if (result.Successful)
            {
                ShowErrors = false;
                Success = true;
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
