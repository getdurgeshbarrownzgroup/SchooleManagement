using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain.Entities
{
    public  class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }= default!;
   
        public int Description { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
