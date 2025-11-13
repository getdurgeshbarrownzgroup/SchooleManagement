using System;
using System.Collections.Generic;
using System.Text;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Services
{
    public class TeacherServices : ITeacherService
    {
        private readonly IGenericRepository<Teacher> _repo;
        private readonly ICacheService _cache;
        private readonly string ALL_TEACHERS_CACHE_KEY = "all_teachers";
        public TeacherServices(IGenericRepository<Teacher> repo, ICacheService cache)
        {
            _repo = repo;
            _cache = cache;
        }
        public async Task<TeacherDto> CreateAsync(CreateTeacherDto dto)
        {
            var teacher = new Teacher
            {
                Id = Guid.NewGuid(),
                FullName= dto.FullName,
               
                Email = dto.Email,
                Subject = dto.Subject
            };
            await _repo.AddAsync(teacher);
            await _cache.RemoveAsync(ALL_TEACHERS_CACHE_KEY);
            return new TeacherDto(teacher.Id, teacher.FullName, teacher.Email, teacher.Subject);

        }

        public async Task<IEnumerable<TeacherDto>> GetAllAsync()
        {
            var cached = await _cache.GetAsync<IEnumerable<TeacherDto>>(ALL_TEACHERS_CACHE_KEY);
            if (cached is not null)
            {
                return cached;
            }

            var teachers = await _repo.GetAllAsync();
            var dtos = teachers.Select(t => new TeacherDto(
                t.Id,
                t.FullName,
                t.Email,
                t.Subject
            ));
            await _cache.SetAsync(ALL_TEACHERS_CACHE_KEY, dtos, TimeSpan.FromMinutes(10));
            return dtos;
        }

        public async Task<TeacherDto?> GetByIdAsync(Guid id)
        {
            var cached = await _cache.GetAsync<TeacherDto>($"teacher_{id}");
            if (cached is not null)
            {
                return cached;
                
            }
            var teacher = await _repo.GetByIdAsync(id);


            if (teacher == null)
            {
                return null;
            }
           var dto= new TeacherDto(
                teacher.Id,
                teacher.FullName,
                teacher.Email,
                teacher.Subject
            );
            await _cache.SetAsync($"teacher_{id}", dto, TimeSpan.FromMinutes(10));
            return dto;
        }

    }
}
