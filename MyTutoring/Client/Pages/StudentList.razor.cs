using Models.ViewModels;
using System.Security.Claims;

namespace MyTutoring.Client.Pages
{
    public partial class StudentList
    {
        IEnumerable<StudentSingleViewModel> Students;
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
            Students = await UserService.GetStudents();
            _loading = true;
        }

        private void OnSubmit()
        {
            Students = Students.Where(x => x.StudentName.Contains(Finder.Name));
            StateHasChanged();
        }

        private void Navigate(string userId)
        {
            NavigationManager.NavigateTo("changeVisibility/" + userId); 
        }

        private void Delete(string userId)
        {
            //deleteStudent
        }
    }

    class Finder
    {
        public string Name { get; set; }
    }
}
