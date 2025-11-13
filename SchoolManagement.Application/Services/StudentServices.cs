using System;
using System.Collections.Generic;
using System.Text;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Services
{
    public class StudentServices:IStudentService
    {
        private readonly IGenericRepository<Student> _repo;
        private readonly ICacheService _cache;
        private const string ALL_STUDENTS_CACHE_KEY = "all_students";
        public StudentServices(IGenericRepository<Student> repo, ICacheService cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<StudentDto> CreateAsync(CreateStudentDto dto)
        {
            var student = new Student
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                AssignedTeacherId = dto.AssignedTeacherId
            };

            await _repo.AddAsync(student);
            await _cache.RemoveAsync(ALL_STUDENTS_CACHE_KEY);
            return new StudentDto(student.Id,student.FirstName, student.LastName,student.Email,student.AssignedTeacherId);
        }


        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {

            var cached=await _cache.GetAsync<IEnumerable<StudentDto>> (ALL_STUDENTS_CACHE_KEY);

            if (cached is not null)
            {
                return cached;
            }


            var student = await _repo.GetAllAsync();
            var dtoList= student.Select(s => new StudentDto(
                s.Id,
                s.FirstName,
                s.LastName,
                s.Email,
                s.AssignedTeacherId
            ));

            await _cache.SetAsync(ALL_STUDENTS_CACHE_KEY, dtoList, TimeSpan.FromMinutes(10));
            return dtoList;
        }

        public async Task<StudentDto?> GetByIdAsync(Guid id)
        {
            var cacheKey = $"student_{id}";
            var cached = await _cache.GetAsync<StudentDto>(cacheKey);
            if(cached is not null)
            {
                return cached;
            }
            var student = await _repo.GetByIdAsync(id);
            if (student == null)
            {
                return null;
            }

            var dto = new StudentDto(student.Id, student.FirstName, student.LastName, student.Email, student.AssignedTeacherId);
            await _cache.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(10));
            return dto;

            
        }


    }
}
