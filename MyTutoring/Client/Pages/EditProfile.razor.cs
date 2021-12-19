using Models.ViewModels;

namespace MyTutoring.Client.Pages
{
    public partial class EditProfile
    {
        public EditProfileViewModel model { get; set; } = new EditProfileViewModel();

        public bool ShowErrors { get; set; }
        public bool Success { get; set; } = false;
        string Error { get; set; }

        private bool rendered = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            rendered = true;
            StateHasChanged();
        }

        public async Task OnSubmit()
        {       
            var result = await EditProfileService.EditProfile(model);

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

        private async Task LoadData()
        {
            model = await EditProfileService.GetEditProfileModel();
        }
    }
}
