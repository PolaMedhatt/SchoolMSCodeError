using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repos.Interfaces;

namespace SchoolManagementSystem.Repos.Implementation
{
    public class ClassRepo : GenericRepo<Class>, IClassRepo
    {
        public ClassRepo(AppDbContext db) : base(db) { }
    }
}
