using Microsoft.AspNetCore.Components;
using Models.ViewModels;

namespace MyTutoring.Client.Pages
{
    public partial class ChangeGrade
    {
        [Parameter]
        public int HomeworkId { get; set; }

        private ChangeGradeViewModel changeGrade = new ChangeGradeViewModel();
        private bool isLoading = false;
        public bool ShowErrors { get; set; }
        public bool Success { get; set; } = false;
        string Error { get; set; }

        protected override Task OnParametersSetAsync()
        {
            StateHasChanged();
            OnInitializedAsync();
            return base.OnParametersSetAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            changeGrade = await HomeworkService.GetHomeworkToChangeGrade(HomeworkId);
            isLoading = true;
            StateHasChanged();
        }

        public async Task OnSubmit()
        {
            var result = await HomeworkService.ChangeGrade(changeGrade);

            if (result.Successful)
            {
                ShowErrors = false;
                Success = true;
                NavigationManager.NavigateTo("Homeworks/" + changeGrade.Id);
            }
            else
            {
                Error = result.Message;
                ShowErrors = true;
            }
        }
    }
}
