using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs
{

    public record StudentDto(Guid Id, string FirstName, string LastName, string Email, Guid? AssignedTeacherId);
     
}
