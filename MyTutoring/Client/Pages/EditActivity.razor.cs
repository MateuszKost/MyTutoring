using Microsoft.AspNetCore.Components;
using Models.Models;
using Models.ViewModels;

namespace MyTutoring.Client.Pages
{
    public partial class EditActivity
    {
        [Parameter]
        public int ActivityId { get; set; }

        public ActivitySingleViewModel model = new ActivitySingleViewModel();
        private IEnumerable<StudentSingleViewModel> Students;
        private Dictionary<int, string> days = ActivityTimeList.CreateDayOfWeek();
        private IEnumerable<float> startTimeList = ActivityTimeList.CreateActivityTimeList();
        private IEnumerable<float> endTimeList = ActivityTimeList.CreateActivityTimeList();

        public bool ShowErrors { get; set; }
        public bool Success { get; set; } = false;
        string Error { get; set; }

        public string SelectedStudentId { get; set; }
        public string SelectedTutorId { get; set; }
        public int SelectedDay { get; set; }
        public float SelectedStartTime { get; set; }
        public float SelectedEndTime { get; set; }



        protected override async Task OnInitializedAsync()
        {
            var state = await AuthState.GetAuthenticationStateAsync();
            var user = state.User;
            var userId = user.FindFirst("id");
            model = await ActivitiesService.GetActivity(new SingleItemByIdRequest() { Id = ActivityId });

            SelectedTutorId = userId.Value;
            SelectedStudentId = model.StudentId;
            SelectedStartTime = model.StartTime;
            SelectedEndTime = model.EndTime;
            SelectedDay = days.SingleOrDefault(d => d.Value == model.DayOfWeek).Key;
            Students = await UserService.GetStudents();
        }        

        private void OnSelectDay(ChangeEventArgs e)
        {
            SelectedDay = Int32.Parse(e.Value.ToString());
            Console.WriteLine(SelectedStudentId);
        }

        private void OnSelectStartTime(ChangeEventArgs e)
        {
            SelectedStartTime = float.Parse(e.Value.ToString());
            endTimeList = endTimeList.Where(s => s > SelectedStartTime);
            Console.WriteLine(SelectedStudentId);
        }

        private void OnSelectEndTime(ChangeEventArgs e)
        {
            SelectedEndTime = float.Parse(e.Value.ToString());
            startTimeList = startTimeList.Where(s => s < SelectedEndTime);
            Console.WriteLine(SelectedStudentId);
        }

        private async void Edit()
        {
            ShowErrors = false;

            if (SelectedStudentId == "0")
            {
                Error = "Wybierz studenta i grupe materiału";
                ShowErrors = true;
            }

            model.StudentId = SelectedStudentId;
            model.TutorId = SelectedTutorId;
            model.StartTime = SelectedStartTime;
            model.EndTime = SelectedEndTime;
            model.DayOfWeek = days[SelectedDay];

            var result = await ActivitiesService.EditActivity(model);

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
