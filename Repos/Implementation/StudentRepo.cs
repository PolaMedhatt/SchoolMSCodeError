using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repos.Interfaces;

namespace SchoolManagementSystem.Repos.Implementation
{
    public class StudentRepo : GenericRepo<Student>, IStudentRepo
    {
        public StudentRepo(AppDbContext db) : base(db) { }


        public async Task<Student> GetStudentWithProfileAndSubjects(int id)
        {
            return await db.Students
                .Include(s => s.StudentProfile)
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Subject)
                .Include(s => s.Class)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Student>> GetStudentsByGrade()
        {
            return await db.Students
                .Include(s => s.Class)
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Subject)
                .Include(s => s.StudentProfile)
                .OrderByDescending(s => s.StudentProfile.DateOfBirth) 
                .ToListAsync();
        }

    }
}
