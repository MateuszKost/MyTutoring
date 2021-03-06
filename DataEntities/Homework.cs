#nullable disable

namespace DataEntities
{
    public class Homework
    {
        public Homework()
        {
            Materials = new HashSet<Material>();
            TaskSolutions = new HashSet<TaskSolution>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public float Grade { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid TutorId { get; set; }
        public Guid StudentId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Tutor Tutor { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
        public virtual ICollection<TaskSolution> TaskSolutions { get; set; }
    }
}
