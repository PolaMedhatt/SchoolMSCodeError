using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repos.Interfaces;

namespace SchoolManagementSystem.Repos.Implementation
{
    public class StudentProfileRepo : GenericRepo<StudentProfile>, IStudentProfileRepo
    {
        public StudentProfileRepo(AppDbContext db) : base(db) { }
    }
}
