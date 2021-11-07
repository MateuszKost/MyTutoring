using System.ComponentModel.DataAnnotations.Schema;

namespace DataEntities
{
    public class Teacher
    {
        public Teacher()
        {
            Activities = new HashSet<Activity>();
            StudentsTeachers = new HashSet<StudentTeacher>();
            Homeworks = new HashSet<Homework>();
        }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<StudentTeacher> StudentsTeachers { get; set; }
        public virtual ICollection<Homework> Homeworks { get; set; }
    }
}
