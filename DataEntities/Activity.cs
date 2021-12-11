#nullable disable

namespace DataEntities
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float StartTime { get; set; }
        public float EndTime { get; set; }
        public int DayOfWeek { get; set; }
        public Guid StudentId { get; set; }
        public Guid TutorId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Tutor Tutor { get; set; }
    }
}
