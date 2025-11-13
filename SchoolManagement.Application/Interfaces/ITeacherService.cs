using System;
using System.Collections.Generic;
using System.Text;
using SchoolManagement.Application.DTOs;

namespace SchoolManagement.Application.Interfaces
{
    public  interface ITeacherService
    {
        Task<TeacherDto> CreateAsync(CreateTeacherDto dto);
        Task<IEnumerable<TeacherDto>> GetAllAsync();
        Task<TeacherDto?> GetByIdAsync(Guid id);

    }
}
