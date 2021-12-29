using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Models.ViewModels;
using Services.FileConverter;

namespace MyTutoring.Client.Pages
{
    public partial class AddSolution
    {
        [Parameter]
        public int HomeworkId { get; set; }

        public readonly SolutionViewModel model = new SolutionViewModel();
        public bool ShowErrors { get; set; }
        public bool Success { get; set; } = false;
        string Error { get; set; }

        protected override async Task OnInitializedAsync()
        {
           
        }


        private async void LoadFile(InputFileChangeEventArgs eventArgs)
        {
            IBrowserFile file = eventArgs.File;
            model.DataTaskSolution = await FileConverter.IBrowserFileImageToBase64Async(file);
            model.TaskSolutionFileName = file.Name;
        }

        private async void UploadFiles()
        {
            ShowErrors = false;

            model.HomeworkId = HomeworkId;

            var result = await HomeworkService.AddTaskSolution(model);

            if (result.Successful)
            {
                ShowErrors = false;
                Success = true;
                StateHasChanged();

                NavigationManager.NavigateTo("Homeworks/" + model.HomeworkId);
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
