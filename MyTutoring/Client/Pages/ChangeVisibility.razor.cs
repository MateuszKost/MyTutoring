using Microsoft.AspNetCore.Components;
using Models.ViewModels;

namespace MyTutoring.Client.Pages
{
    public partial class ChangeVisibility
    {
        [Parameter]
        public string UserId { get; set; }

        private bool _loading = false;
        public ICollection<VisibilitySingleViewModel> Visibilities;

        protected override async Task OnInitializedAsync()
        {
            Visibilities = await MaterialVisibilityService.GetVisibilityList(UserId);
            _loading = true;
        }

        private async void OnSubmit()
        {
            var result = await MaterialVisibilityService.SetVisibilityList(Visibilities);
            StateHasChanged();
        }
    }
}
