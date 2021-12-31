namespace Models.ViewModels
{
    public class ActivitySingleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? UserName { get; set; }
        public float StartTime { get; set; }
        public float EndTime { get; set; }
        public string DayOfWeek { get; set; }

        public string? StudentId { get; set; }
        public string? TutorId { get; set; }
    }
}
