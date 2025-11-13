using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Application.DTOs;

namespace SchoolManagement.Application.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDto> CreateAsync(CreateStudentDto dto);
        Task<StudentDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<StudentDto>> GetAllAsync(); 
   
    }
}
