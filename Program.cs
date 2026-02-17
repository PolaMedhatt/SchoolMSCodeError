using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Repos.Interfaces;
using SchoolManagementSystem.Repos.Implementation;

namespace SchoolManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("con")));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepo<>));
            
            builder.Services.AddScoped<IClassRepo, ClassRepo>();
            builder.Services.AddScoped<IStudentRepo, StudentRepo>();
            builder.Services.AddScoped<ITeacherRepo, TeacherRepo>();
            builder.Services.AddTransient<IStudentProfileRepo, StudentProfileRepo>();
            builder.Services.AddScoped<ISubjectRepo, SubjectRepo>();

        
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

           
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
