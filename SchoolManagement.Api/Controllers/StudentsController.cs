using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentservice)
        {
            _studentService = studentservice;
        }
        [HttpGet]

        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        // create student 
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDto dto)
        {
            if (!ModelState.IsValid)
            {
                               return BadRequest(ModelState);

            }

            var createdStudent = await _studentService.CreateAsync(dto);
            return Ok(createdStudent);

        }


        [HttpGet("id:guid")]
        public async Task<IActionResult> GetStudentById([FromRoute] Guid id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student is null)
            {
                return NotFound();
            }
            return Ok(student);
        }

     

        

    }
}
