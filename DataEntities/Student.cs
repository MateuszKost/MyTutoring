using System.ComponentModel.DataAnnotations.Schema;

namespace DataEntities
{
    public class Student
    {
        public Student()
        {
            Activities = new HashSet<Activity>();
            StudentsTeachers = new HashSet<StudentTeacher>();
            Homeworks = new HashSet<Homework>();
            MaterialsGroupVisibilities = new HashSet<MaterialsGroupVisibility>();
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
        public virtual ICollection<MaterialsGroupVisibility> MaterialsGroupVisibilities { get; set; }
    }
}
