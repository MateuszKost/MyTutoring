using Models.Models;
using Models.ViewModels;
using System.Security.Claims;

namespace MyTutoring.Client.Pages
{
    public partial class HomeworkOpen
    {
        public IEnumerable<HomeworkSingleViewModel> Homeworks;
        private bool _loading = false;
        Finder finder = new Finder();
               
        protected override async Task OnInitializedAsync()
        {
            var state = await AuthState.GetAuthenticationStateAsync();
            var user = state.User;
            var userId = user.FindFirst("id");
            var role = user.FindFirst(ClaimTypes.Role);

            Homeworks = await HomeworkService.GetHomeworkViewModelList(new HomeworkRequest { UserId = userId.Value, UserRole = role.Value, Status = false });
            _loading = true;
        }

        private void OnSubmit()
        {
            Homeworks = Homeworks.Where(x => x.Name.Contains(finder.Name));
            StateHasChanged();
        }

        private void Navigate(int? homeworkId)
        {
            NavigationManager.NavigateTo("Homeworks/" + homeworkId);
        }
    }    
}
