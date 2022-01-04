using Models.ViewModels;
using System.Security.Claims;

namespace MyTutoring.Client.Pages
{
    public partial class TutorList
    {
        IEnumerable<StudentSingleViewModel> Tutors;
        Finder Finder = new Finder();
        bool _loading = false;
        string roleString;

        protected override async Task OnInitializedAsync()
        {
            var state = await AuthState.GetAuthenticationStateAsync();
            var user = state.User;
            var userId = user.FindFirst("id");
            var role = user.FindFirst(ClaimTypes.Role);
            roleString = role.Value;
            Tutors = await UserService.GetTutors();
            _loading = true;
        }

        private void OnSubmit()
        {
            Tutors = Tutors.Where(x => x.StudentName.Contains(Finder.Name));
            StateHasChanged();
        }

        private void Navigate(string userId)
        {
            NavigationManager.NavigateTo("changeVisibility/" + userId); 
        }

        private void Delete(string userId)
        {
            //deleteTutor
        }
    }
}
