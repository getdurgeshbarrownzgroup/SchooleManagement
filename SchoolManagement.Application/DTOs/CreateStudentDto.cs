using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs
{

    public record CreateStudentDto(string FirstName , string LastName, string Email, DateTime DateOfBirth, Guid? AssignedTeacherId);
     
}
