using Microsoft.AspNetCore.Components;
using Models.ViewModels;
using System.Security.Claims;

namespace MyTutoring.Client.Pages
{
    public partial class Homeworks
    {
        [Parameter]
        public int HomeworkId { get; set; }

        public string roleString { get; set; }

        private HomeworkSingleViewModel Homework = new HomeworkSingleViewModel();
        private bool isLoading = false;

        protected override Task OnParametersSetAsync()
        {
            StateHasChanged();
            OnInitializedAsync();
            return base.OnParametersSetAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            var state = await AuthState.GetAuthenticationStateAsync();
            var user = state.User;
            var role = user.FindFirst(ClaimTypes.Role);
            roleString = role.Value;

            Homework = await HomeworkService.GetHomeworkSingleViewModel(HomeworkId);
            isLoading = true;
            StateHasChanged();
        }

        private void NavigateToGrade(int homeworkId)
        {
            NavigationManager.NavigateTo("changeGrade/" + homeworkId);
        }

        private void NavigateToSolution(int homeworkId)
        {
            NavigationManager.NavigateTo("addSolution/" + homeworkId);
        }
    }
}
