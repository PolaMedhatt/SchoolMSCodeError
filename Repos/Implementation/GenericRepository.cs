using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Repos.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repos.Implementation
{
    public class GenericRepo<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext db;

        public GenericRepo(AppDbContext db)
        {
            db = db;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await db.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int ?id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await db.Set<T>().AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            db.Set<T>().Update(entity);
        }

        public async Task Delete(T entity)
        {
            db.Set<T>().Remove(entity);
        }

        public async Task SaveChanges()
        {
            await db.SaveChangesAsync();
        }
    }
}
