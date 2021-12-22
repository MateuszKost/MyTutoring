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
            Students = await MaterialVisibilityService.GetStudents(); // sciaganie z bazy 
            _loading = true;
        }

        private void OnSubmit()
        {
            Students = Students.Where(x => x.StudentName.Contains(Finder.UserName));
            StateHasChanged();
        }

        private void Navigate(int materialGroupId)
        {
            NavigationManager.NavigateTo("changeVisibility/" + materialGroupId); // zmienic
        }
    }

    class Finder
    {
        public string UserName { get; set; }
    }
}
