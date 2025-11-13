using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs
{
    public record TeacherDto(Guid Id, string FullName, string Email,string Subject);

}
