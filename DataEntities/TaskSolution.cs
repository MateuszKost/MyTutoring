#nullable disable

namespace DataEntities
{
    public class TaskSolution
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileSha1 { get; set; }
        public int HomeworkId { get; set; }

        public virtual Homework Homework { get; set; }
    }
}
