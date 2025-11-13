using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }= DateTime.Now;

        public Guid? AssignedTeacherId { get; set; }

        public Teacher? AssignedTeacher { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();


    }
}
