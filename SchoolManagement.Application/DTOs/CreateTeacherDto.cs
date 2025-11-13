using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs
{   
    
    public record CreateTeacherDto( string FullName , string Email, string Subject);
}
