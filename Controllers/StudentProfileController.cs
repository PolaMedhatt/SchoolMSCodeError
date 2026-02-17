using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.DTOs.StudentProfileDTOs;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repos.Interfaces;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentProfileController : ControllerBase
    {
        private readonly IGenericRepository<StudentProfile> _profileRepo;
        private readonly IGenericRepository<Student> _studentRepo;

        public StudentProfileController(
            IGenericRepository<StudentProfile> profileRepo,
            IGenericRepository<Student> studentRepo)
        {
            _profileRepo = profileRepo;
            _studentRepo = studentRepo;
        }

       
        [HttpPost]
        public async Task<IActionResult> CreateProfile(StudentProfileCreateDto dto)
        {
            var student = await _studentRepo.GetById(dto.StudentId);
            if (student == null) return BadRequest("Student does not exist");

            if (student.StudentProfile != null)
                return BadRequest("Student already has a profile");

            var profile = new StudentProfile
            {
                StudentId = dto.StudentId,
                Address = dto.Address,
                DateOfBirth = dto.DateOfBirth
            };

            await _profileRepo.Add(profile);
            return NotFound();
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            var profiles = await _profileRepo.GetAll();
            var students = await _studentRepo.GetAll();

            var result = profiles.Select(p => new
            {
                p.Id,
                p.Address,
                p.DateOfBirth,
                Student = students.Where(s => s.Id == p.StudentId)
                                  .Select(s => new { s.Id, s.Name, s.Email })
                                  .FirstOrDefault()
            }).ToList();

            return Ok(result);
        }

   
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var profile = await _profileRepo.GetById(id);
            if (profile == null) return NotFound();

            var student = await _studentRepo.GetById(profile.StudentId);

            var result = new
            {
                profile.Id,
                profile.Address,
                profile.DateOfBirth,
                Student = student != null ? new { student.Id, student.Name, student.Email } : null
            };

            return Ok(result);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(int id, StudentProfileUpdateDto dto)
        {
            var profile = await _profileRepo.GetById(id);
            if (profile == null) return NotFound();

            profile.Address = dto.Address;
            profile.DateOfBirth = dto.DateOfBirth;

            await _profileRepo.Update(profile);
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var profile = await _profileRepo.GetById(id);
            if (profile == null) return NotFound();

            await _profileRepo.Delete(profile);
            return NoContent();
        }
    }
}
