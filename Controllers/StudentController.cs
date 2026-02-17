using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.DTOs.StudentDTOS;
using SchoolManagementSystem.DTOs.StudentProfileDTOs;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repos.Interfaces;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepo _studentRepo;
        private readonly IClassRepo _classRepo;

        public StudentsController(IStudentRepo studentRepo, IClassRepo classRepo)
        {
            _studentRepo = studentRepo;
            _classRepo = classRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentCreateDto dto)
        {
            var exists = (await _studentRepo.GetAll())?.Any(s => s.Email == dto.Email) ?? false;
            if (exists) return BadRequest("Email must be unique");

            var student = new Student
            {
                Name = dto.Name ?? null,
                Email = dto.Email ?? null,
                Phone = dto.Phone ?? null,
                ClassId = dto.ClassId,
                StudentProfile = dto.Address != null || dto.DateOfBirth != null ? new StudentProfile
                {
                    Address = dto.Address ?? null,
                    DateOfBirth = dto.DateOfBirth
                } : null
            };

            await _studentRepo.Add(student);
            await _studentRepo.Add(student);
            return StatusCode(204);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentRepo.GetStudentsByGrade() ?? new List<Student>();
            var result = students
                .GroupBy(s => s.Class.Grade)
                .Select(g => new
                {
                    Grade = g.Key,
                    Count = g.Count(),
                    Students = g.Select(s => new StudentReadDto
                    {
                        Id = s?.Id ?? 0,
                        Name = s?.Name ?? null,
                        Email = s?.Email ?? null,
                        Phone = s?.Phone ?? null,
                        ClassName = s?.Class?.Name ?? null,
                        Subjects = s?.StudentSubjects?.Select(ss => ss?.Subject?.Name ?? null).ToList() ?? new List<string>(),
                        Profile = s?.StudentProfile != null ? new StudentProfileReadDto
                        {
                            Id = s.StudentProfile?.Id ?? 0,
                            Address = s.StudentProfile?.Address ?? null,
                            DateOfBirth = s.StudentProfile.DateOfBirth
                        } : null
                    }).ToList()
                });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentRepo.GetStudentWithProfileAndSubjects(id);
            if (student == null) return NotFound();

            var dto = new StudentReadDto
            {
                Id = student.Id ,
                Name = student?.Name ,
                Email = student?.Email,
                Phone = student?.Phone ,
                ClassName = student?.Class?.Name ,
                Subjects = student?.StudentSubjects?.Select(ss => ss?.Subject?.Name ?? null).ToList() ?? new List<string>(),
                Profile = student?.StudentProfile != null ? new StudentProfileReadDto
                {
                    Id = student.StudentProfile?.Id ?? 0,
                    Address = student.StudentProfile?.Address ?? null,
                    DateOfBirth = student.StudentProfile.DateOfBirth
                } : null
            };
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentUpdateDto dto)
        {
            var student = await _studentRepo.GetById(id);
            if (student == null) return NotFound();

            student.Name = dto?.Name ?? student.Name;
            student.Email = dto?.Email ?? student.Email;
            student.Phone = dto?.Phone ?? student.Phone;

            await _studentRepo.Update(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentRepo.GetById(id);
            if (student == null) return NotFound();

            if (student?.ClassId != null)
                return BadRequest("Cannot delete student: student is enrolled in a class");

            await _studentRepo.Delete(student);
            return NoContent();
        }
    }
}
