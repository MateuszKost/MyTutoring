﻿using Models;

namespace MyTutoring.Client.Pages
{
    public partial class RegisterTutor
    {
        public readonly RegisterModel model = new RegisterModel();

        public bool ShowErrors { get; set; }

        public bool Success { get; set; } = false;

        HttpContent Error { get; set; }

        public async Task OnSubmit()
        {
            model.AccountType = "tutor";
            var result = await AuthService.Register(model);

            if (result.IsSuccessStatusCode)
            {
                ShowErrors = false;
                Success = true;
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Error = result.Content;
                ShowErrors = true;
            }
        }
    }
}
