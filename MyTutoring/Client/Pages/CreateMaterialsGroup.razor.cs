using Models;

namespace MyTutoring.Client.Pages
{
    public partial class CreateMaterialsGroup
    {
        public readonly MaterialGroupSingleViewModel model = new MaterialGroupSingleViewModel();

        public bool ShowErrors { get; set; }
        public bool Success { get; set; } = false;
        string Error { get; set; }
                
        public async Task OnSubmit()
        {
            var result = await MaterialsGroupService.CreateMaterialsGroup(model);

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
