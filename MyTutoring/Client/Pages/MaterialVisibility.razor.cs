using Models.ViewModels;

namespace MyTutoring.Client.Pages
{
    public partial class MaterialVisibility
    {
        IEnumerable<StudentSingleViewModel> Students;
        Finder Finder = new Finder();
        bool _loading = false;

        protected override async Task OnInitializedAsync()
        {
            Students = await MaterialVisibilityService.GetStudents();
            _loading = true;
        }

        private void OnSubmit()
        {
            Students = Students.Where(x => x.StudentName.Contains(Finder.UserName));
            StateHasChanged();
        }

        private void Navigate(int userId)
        {
            NavigationManager.NavigateTo("changeVisibility/" + userId); 
        }
    }

    class Finder
    {
        public string UserName { get; set; }
    }
}
