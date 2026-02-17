using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repos.Implementation;
using SchoolManagementSystem.Repos.Interfaces;

namespace SchoolManagementSystem.Repos.Interfaces
{
    public interface IStudentRepo : IGenericRepository<Student>
    {
        Task<Student> GetStudentWithProfileAndSubjects(int id);
        Task<IEnumerable<Student>> GetStudentsByGrade();
    }
}
