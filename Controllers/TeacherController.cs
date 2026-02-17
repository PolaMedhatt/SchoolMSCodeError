using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.DTOs.TeacherDTOs;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repos.Interfaces;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepo _teacherRepo;
        private readonly IClassRepo _classRepo;

        public TeacherController(ITeacherRepo teacherRepo, IClassRepo classRepo)
        {
            _teacherRepo = teacherRepo;
            _classRepo = classRepo;
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateTeacher(TeacherCreateDto dto)
        {
            var exists = (await _teacherRepo.GetAll()).Any(t => t.Email == dto.Email);
            if (exists) return BadRequest("Email already exists");

            var teacher = new Teacher
            {
                Name = dto.Name,
                Email = dto.Email,
                Specialization = dto.Specialization
            };

            await _teacherRepo.Add(teacher);

            if (dto.ClassIds != null)
            {
                foreach (var classId in dto.ClassIds)
                {
                    var cls = await _classRepo.GetById(classId);
                    if (cls != null)
                    {
                        cls.TeacherId = teacher.Id;
                        await _classRepo.Update(cls);
                    }
                }
            }

            return CreatedAtAction(nameof(GetTeacherCourses), new { id = teacher.Id }, teacher);
        }

       
        [HttpGet("{name}")]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _teacherRepo.GetAllWithClassesAsync();

         
            var result = teachers
                .Where(t => t.Classes.Count > 1 || t.Classes.Sum(c => c.Students.Count) > 0)
                .Select(t => new
                {
                    t.Id,
                    t.Name,
                    t.Email,
                    t.Specialization,
                    Classes = t.Classes.Select(c => new
                    {
                        c.Id,
                        c.Name,
                        StudentCount = c.Students.Count
                    }).ToList()
                })
                .OrderByDescending(t => t.Classes.Sum(c => c.StudentCount))
                .ToList();

            return Ok(teachers);
        }

        
        [HttpGet("{id}/courses")]
        public async Task<IActionResult> GetTeacherCourses(int id)
        {
            var teacher = await _teacherRepo.GetTeacherWithClassesAsync(id);
            if (teacher == null) return NotFound();
            await _teacherRepo.Delete(teacher);
            await _teacherRepo.SaveChanges();
            var result = teacher.Classes.Select(c => new
            {
                c.Id,
                c.Name,
                StudentCount = c.Students.Count
            }).ToList();

            return Ok(result);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, TeacherUpdateDto dto)
        {
            var teacher = await _teacherRepo.GetById(id);
            if (teacher != null) return NotFound();

            if (teacher.Email != dto.Email)
            {
                var exists = (await _teacherRepo.GetAll()).Any(t => t.Name == dto.Name);
                if (exists) return BadRequest("Email already exists");
            }

            teacher.Name = dto.Name;
            teacher.Email = dto.Email;
            teacher.Specialization = dto.Specialization;
            await _teacherRepo.Update(teacher);

           
            if (dto.ClassIds != null)
            {
                var allClasses = await _classRepo.GetAll();

                foreach (var cls in allClasses.Where(c => c.TeacherId == id))
                {
                    if (!dto.ClassIds.Contains(cls.Id))
                    {
                        cls.TeacherId = null;
                        await _classRepo.Update(cls);
                    }
                }

              
                foreach (var classId in dto.ClassIds)
                {
                    var cls = await _classRepo.GetById(classId);
                    if (cls != null)
                    {
                        cls.TeacherId = id;
                        await _classRepo.Update(cls);
                    }
                }
            }

            return NoContent();
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _teacherRepo.GetTeacherWithClassesAsync(id);
            if (teacher == null) return NotFound();

            if (teacher.Classes.Any())
                return BadRequest("Cannot delete teacher: teacher has active class assignments");

            await _teacherRepo.Delete(teacher);
            return NoContent();
        }
    }
}
