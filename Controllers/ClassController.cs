using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.DTOs.ClassDTOS;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repos.Interfaces;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IGenericRepository<Class> _classRepo;
        private readonly IGenericRepository<Teacher> _teacherRepo;
        private readonly IGenericRepository<Student> _studentRepo;

        public ClassController(
            IGenericRepository<Class> classRepo,
            IGenericRepository<Teacher> teacherRepo,
            IGenericRepository<Student> studentRepo)
        {
            _classRepo = classRepo;
            _teacherRepo = teacherRepo;
            _studentRepo = studentRepo;
        }

      
        [HttpPost]
        public async Task<IActionResult> CreateClass(ClassCreateDto dto)
        {
            if (dto.Capacity <= 0) return BadRequest("Capacity must be greater than 0");

            var teacher = await _teacherRepo.GetById(dto.TeacherId);
            if (teacher == null) return BadRequest("Teacher does not exist");

            var cls = new Class
            {
                Name = dto.Name,
                Grade = dto.Grade,
                Capacity = dto.Capacity,
                TeacherId = dto.TeacherId
            };

            await _classRepo.Add(cls);
            return StatusCode(201);
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            var classes = await _classRepo.GetAll();
            var students = await _studentRepo.GetAll();
            var teachers = await _teacherRepo.GetAll();

            var result = classes.Select(c => new
            {
                c.Id,
                c.Name,
                c.Grade,
                c.Capacity,
                Teacher = teachers.FirstOrDefault(t => t.Id == c.TeacherId)?.Name,
                EnrolledStudents = students.Where(s => s.ClassId == c.Id).Select(s => new
                {
                    s.Id,
                    s.Name,
                    s.Email
                }).ToList(),
                RemainingCapacity = c.Capacity - students.Count(s => s.ClassId == c.Id)
            })
            .OrderByDescending(c => c.EnrolledStudents.Count)
            .ToList();

            return Ok(result);
        }

        // GET /api/classes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClass(int id)
        {
            var cls = await _classRepo.GetById(id);
            if (cls == null) return NotFound();


            var teacher = await _teacherRepo.GetById(cls.TeacherId);
            var students = (await _studentRepo.GetAll())
                .Where(s => s.ClassId == id)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    s.Email,
                    Profile = s.StudentProfile != null ? new
                    {
                        s.StudentProfile.Id,
                        s.StudentProfile.Address,
                        s.StudentProfile.DateOfBirth
                    } : null
                }).ToList();

            var result = new
            {
                cls.Id,
                cls.Name,
                cls.Grade,
                cls.Capacity,
                Teacher = teacher != null ? new { teacher.Id, teacher.Name, teacher.Email } : null,
                Students = students,
                RemainingCapacity = cls.Capacity - students.Count
            };

            return Ok(result);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(int id, ClassUpdateDto dto)
        {
            var cls = await _classRepo.GetById(id);
            if (cls == null) return NotFound();

            var studentsCount = (await _studentRepo.GetAll()).Count(s => s.ClassId == id);
            if (dto.Capacity < studentsCount) return BadRequest("Cannot reduce capacity below current number of students");

            cls.Name = dto.Name;
            cls.Grade = dto.Grade;
            cls.Capacity = dto.Capacity;

            if (dto.TeacherId != cls.TeacherId)
            {
                var teacher = await _teacherRepo.GetById(dto.TeacherId);
                if (teacher == null) return BadRequest("Teacher does not exist");
                cls.TeacherId = dto.TeacherId;
            }

            await _classRepo.Update(cls);
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var cls = await _classRepo.GetById(id);
            if (cls == null) return NotFound();

            var hasStudents = (await _studentRepo.GetAll()).Any(s => s.ClassId == id);
            if (hasStudents) return BadRequest("Cannot delete class with enrolled students");

            await _classRepo.Delete(cls);
            return NoContent();
        }
    }
}
