using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repos.Interfaces;

namespace SchoolManagementSystem.Repos.Implementation
{
    public class TeacherRepo : GenericRepo<Teacher>, ITeacherRepo
    {
        private readonly AppDbContext _context;

        public TeacherRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Teacher>> GetAllWithClassesAsync()
        {
            return await _context.Teachers
                .Include(t => t.Classes)
                    .ThenInclude(c => c.Students)
                .ToListAsync();
        }

        public async Task<Teacher?> GetTeacherWithClassesAsync(int id)
        {
            return await _context.Teachers
                .Include(t => t.Classes)
                    .ThenInclude(c => c.Students)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
