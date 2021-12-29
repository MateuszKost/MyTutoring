#nullable disable

namespace DataEntities
{
    public class TaskSolution
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int HomeworkId { get; set; }

        public virtual Homework Homework { get; set; }
    }
}
