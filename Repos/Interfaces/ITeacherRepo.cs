using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Repos.Interfaces
{
    public interface ITeacherRepo : IGenericRepository<Teacher>
    {
        Task<IEnumerable<Teacher>> GetAllWithClassesAsync();
        Task<Teacher?> GetTeacherWithClassesAsync(int id);
    }
}
