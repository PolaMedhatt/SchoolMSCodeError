using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repos.Implementation;

namespace SchoolManagementSystem.Repos.Interfaces
{
    public interface ISubjectRepo : IGenericRepository<Subject>
    {
        Task<List<Subject>> GetSubjects();
        Task<Subject> GetSubject(int id);
    }
}
