using Models;

namespace MyTutoring.Client.Pages
{
    public partial class RegisterTutor
    {
        public readonly RegisterModel model = new RegisterModel();

        public bool ShowErrors { get; set; }

        public bool Success { get; set; } = false;

        string Error { get; set; }

        public async Task OnSubmit()
        {
            model.AccountType = "tutor";
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
