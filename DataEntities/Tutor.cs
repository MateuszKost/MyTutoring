﻿#nullable disable

using System.ComponentModel.DataAnnotations.Schema;

namespace DataEntities
{
    public class Tutor
    {
        public Tutor()
        {
            Activities = new HashSet<Activity>();
            StudentsTutors = new HashSet<StudentTutor>();
            Homeworks = new HashSet<Homework>();
        }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<StudentTutor> StudentsTutors { get; set; }
        public virtual ICollection<Homework> Homeworks { get; set; }
    }
}