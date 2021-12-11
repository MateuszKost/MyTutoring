#nullable disable

namespace DataEntities
{
    public class StudentTutor
    {
        public Guid StudentId { get; set; }
        public Guid TutorId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Tutor Tutor { get; set; }
    }
}
