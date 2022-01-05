using Models.Models;
using Models.ViewModels;
using System.Security.Claims;

namespace MyTutoring.Client.Pages
{
    public partial class Calendar
    {
        private bool _loading = false;
        public IEnumerable<ActivitySingleViewModel> Activities;
        Finder finder = new Finder();
        string roleString;

        protected override async Task OnInitializedAsync()
        {
            var state = await AuthState.GetAuthenticationStateAsync();
            var user = state.User;
            var userId = user.FindFirst("id");
            var role = user.FindFirst(ClaimTypes.Role);
            roleString = role.Value;

            Activities = await ActivitiesService.GetActivitiesList(new UserInfo { Id = userId.Value, Role = roleString });
            _loading = true;
        }

        private void OnSubmit()
        {
            Activities = Activities.Where(x => x.Name.Contains(finder.Name));
            StateHasChanged();
        }

        private void NavigateToEditActivity(int activityId)
        {
            NavigationManager.NavigateTo("calendar/editActivity" + activityId);
        }

        private void Delete(int activityId)
        {
            ActivitiesService.DeleteActivity(activityId);
        }
    }
}
