using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.DTOs.Subject_DTOs;

using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repos.Interfaces;
using System.Linq;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepo _subjectRepo;
        private readonly IStudentRepo _studentRepo;

        public SubjectController(IStudentRepo studentRepo, ISubjectRepo subjectRepo)
        {
            _studentRepo = studentRepo;
            _subjectRepo = subjectRepo;
        }


        [HttpPost]
        public async Task<IActionResult> CreateSubject(SubjectCreateDto dto)
        {
            var exists = (await _subjectRepo.GetAll())
                .Any(s => s.Name == dto.Name || s.Code == dto.Code);
            if (exists) return BadRequest("Name or Code already exists");

            var subject = new Subject
            {
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description
            };

            await _subjectRepo.Add(subject);

            
            if (dto.StudentIds != null)
            {
                foreach (var studentId in dto.StudentIds)
                {
                    var student = await _studentRepo.GetById(studentId);
                    if (student != null)
                    {
                        subject.StudentSubjects.Add(new StudentSubject
                        {
                            StudentId = studentId,
                            SubjectId = subject.Id
                        });
                    }
                }
                await _subjectRepo.Update(subject);
            }

            return StatusCode(201);
        }


        [HttpGet]
        public async Task<IActionResult> GetSubjects([FromQuery] int? minStudents)
        {
            var subjects = await _subjectRepo.GetSubjects();
            var students = await _studentRepo.GetAll();

            var result = subjects
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    s.Code,
                    s.Description,
                    Students = students
                        .Where(st => st.StudentSubjects.Any(ss => ss.SubjectId == s.Id))
                        .Select(st => new { st.Id, st.Name, st.Email })
                        .ToList()
                })
                .Where(s => minStudents == null || minStudents == 0 || s.Students.Count > minStudents)

                .OrderByDescending(s => s.Students.Count)
                .ToList();

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            var subject = await _subjectRepo.GetSubject(id);
            if (subject == null) return NotFound();

            var students = (await _studentRepo.GetAll())
                .Where(s => s.StudentSubjects.Any(ss => ss.SubjectId == id))
                .Select(s => new { s.Id, s.Name, s.Email })
                .ToList();

            var result = new
            {
                subject.Id,
                subject.Name,
                subject.Code,
                subject.Description,
                Students = students
            };

            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, SubjectUpdateDto dto)
        {
            var subject = await _subjectRepo.GetById(id); 
            if (subject == null) return NotFound();

            var exists = (await _subjectRepo.GetAll())
                .Any(s => s.Id != id && (s.Name == dto.Name || s.Code == dto.Code));
            if (exists) return BadRequest("Name or Code already exists");

            subject.Name = dto.Name;
            subject.Code = dto.Code;
            subject.Description = dto.Description;

            
            subject.StudentSubjects = new List<StudentSubject>();

            if (dto.StudentIds != null)
            {
                foreach (var studentId in dto.StudentIds.Distinct()) 
                {
                    var student = await _studentRepo.GetById(studentId);
                    if (student != null)
                    {
                        subject.StudentSubjects.Add(new StudentSubject
                        {
                            StudentId = studentId,
                            SubjectId = subject.Id
                        });
                    }
                }
            }

            await _subjectRepo.Update(subject);
            return NoContent();
        }



        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _subjectRepo.GetById(id);
            if (subject == null) return NotFound();

            if (subject.StudentSubjects.Any())
                return BadRequest("Cannot delete subject: students are enrolled");

            await _subjectRepo.Delete(subject);
            return NoContent();
        }
    }
}
