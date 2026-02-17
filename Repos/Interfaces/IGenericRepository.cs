using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repos.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int ?id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task SaveChanges();
    }
}
