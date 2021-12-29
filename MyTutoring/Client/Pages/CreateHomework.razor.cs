using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Models.ViewModels;
using Services.FileConverter;

namespace MyTutoring.Client.Pages
{
    public partial class CreateHomework
    {
        public readonly HomeworkSingleViewModel model = new HomeworkSingleViewModel();
        private IEnumerable<StudentSingleViewModel> Students;
        public bool ShowErrors { get; set; }
        public bool Success { get; set; } = false;
        string Error { get; set; }

        public string SelectedStudentId { get; set; }
        public string SelectedTutorId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var state = await AuthState.GetAuthenticationStateAsync();
            var user = state.User;
            var userId = user.FindFirst("id");

            model.EndTime = DateTime.Now;
            SelectedTutorId = userId.Value;
            Students = await MaterialVisibilityService.GetStudents();
        }

        private void OnSelectStudent(ChangeEventArgs e)
        {
            SelectedStudentId = e.Value.ToString();
            Console.WriteLine(SelectedStudentId);
        }

        private async void LoadFile(InputFileChangeEventArgs eventArgs)
        {
            IBrowserFile file = eventArgs.File;
            model.DataTask = await FileConverter.IBrowserFileImageToBase64Async(file);
            model.FileName = file.Name;
        }

        private async void UploadFiles()
        {
            ShowErrors = false;

            if (SelectedStudentId == "0")
            {
                Error = "Wybierz studenta i grupe materiału";
                ShowErrors = true;
            }

            model.StudentId = SelectedStudentId;
            model.TutorId = SelectedTutorId;

            var result = await HomeworkService.CreateHomework(model);

            if (result.Successful)
            {
                ShowErrors = false;
                Success = true;
                StateHasChanged();


                //NavigationManager.NavigateTo("/");
            }
            else
            {
                Error = result.Message;
                ShowErrors = true;
                StateHasChanged();
            }
        }
    }
}
