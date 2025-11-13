using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Domain.Entities
{
    public  class Teacher
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; }= default!;
        public string Subject { get; set; } = default !;

        public ICollection<Student> students { get; set; } = new List<Student>();

    }
}
