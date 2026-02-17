    using Microsoft.EntityFrameworkCore;
    using SchoolManagementSystem.Models;

    namespace SchoolManagementSystem.Data
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options) { }

            public DbSet<Student> Students { get; set; }
            public DbSet<StudentProfile> StudentProfiles { get; set; }
            public DbSet<Teacher> Teachers { get; set; }
            public DbSet<Class> Classes { get; set; }
            public DbSet<Subject> Subjects { get; set; }
            public DbSet<StudentSubject> StudentSubjects { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Student>()
                    .HasIndex(x => x.Email)
                    .IsUnique();

                modelBuilder.Entity<Class>()
                    .HasIndex(x => x.Name)
                    .IsUnique();

                modelBuilder.Entity<Subject>()
                    .HasIndex(x => x.Name)
                    .IsUnique();

                modelBuilder.Entity<Subject>()
                    .HasIndex(x => x.Code)
                    .IsUnique();

           
                modelBuilder.Entity<Student>()
                    .HasOne(x => x.StudentProfile)
                    .WithOne(x => x.Student)
                    .HasForeignKey<StudentProfile>(x => x.StudentId);


                modelBuilder.Entity<Class>()
                    .HasOne(x => x.Teacher)
                    .WithMany(x => x.Classes)
                    .HasForeignKey(x => x.TeacherId);

           
                modelBuilder.Entity<Student>()
                    .HasOne(x => x.Class)
                    .WithMany(x => x.Students)
                    .HasForeignKey(x => x.ClassId);

            
                modelBuilder.Entity<StudentSubject>()
                    .HasKey(x => new { x.StudentId, x.SubjectId });

                modelBuilder.Entity<StudentSubject>()
                    .HasOne(x => x.Student)
                    .WithMany(x => x.StudentSubjects)
                    .HasForeignKey(x => x.StudentId);

                modelBuilder.Entity<StudentSubject>()
                    .HasOne(x => x.Subject)
                    .WithMany(x => x.StudentSubjects)
                    .HasForeignKey(x => x.SubjectId);


















            modelBuilder.Entity<Teacher>().HasData(
    new Teacher { Id = 1, Name = "Dalia", Email = "ahmed@gmail.com", Specialization = "Computer Science" },
    new Teacher { Id = 2, Name = "Khaled", Email = "mohamed@gmail.com", Specialization = "IT" },
    new Teacher { Id = 3, Name = "Yara", Email = "khaled@gmail.com", Specialization = "Software Eng" }
);

          
            modelBuilder.Entity<Class>().HasData(
                new Class { Id = 1, Name = "s1", Capacity = 20, Grade = "1", TeacherId = 1 },
                new Class { Id = 2, Name = "s2", Capacity = 20, Grade = "2", TeacherId = 2 },
                new Class { Id = 3, Name = "s3", Capacity = 20, Grade = "3", TeacherId = 2 },
                new Class { Id = 4, Name = "s4", Capacity = 20, Grade = "4", TeacherId = 2 },
                new Class { Id = 5, Name = "w1", Capacity = 20, Grade = "1", TeacherId = 3 }
            );

         
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Ahmed", Email = "ahmed@student.com", Phone = "01000000001", ClassId = 1 },
                new Student { Id = 2, Name = "Mohamed", Email = "mohamed@student.com", Phone = "01000000002", ClassId = 2 },
                new Student { Id = 3, Name = "Khaled", Email = "khaled@student.com", Phone = "01000000003", ClassId = 3 },
                new Student { Id = 4, Name = "Asia", Email = "asia@student.com", Phone = "01000000004", ClassId = 4 },
                new Student { Id = 5, Name = "Ashraqat", Email = "ashraqat@student.com", Phone = "01000000005", ClassId = 1 }
            );

            modelBuilder.Entity<Subject>().HasData(
     new Subject { Id = 1, Name = "Cyber Security", Code = "CS101", Description = "Learn cyber security basics" },
     new Subject { Id = 2, Name = "UI/UX", Code = "UI201", Description = "Learn UI and UX design" },
     new Subject { Id = 3, Name = "Code Modification", Code = "MOD301", Description = "Modify and improve code" },
     new Subject { Id = 4, Name = "Embedded Systems", Code = "EMB401", Description = "Intro to embedded systems" }
 );



            modelBuilder.Entity<StudentSubject>().HasData(
                
                new StudentSubject { StudentId = 2, SubjectId = 3 },
                new StudentSubject { StudentId = 2, SubjectId = 4 },
                new StudentSubject { StudentId = 3, SubjectId = 3 },
                new StudentSubject { StudentId = 3, SubjectId = 4 },
                new StudentSubject { StudentId = 4, SubjectId = 3 },
                new StudentSubject { StudentId = 4, SubjectId = 4 },

                
                new StudentSubject { StudentId = 1, SubjectId = 1 },
                new StudentSubject { StudentId = 1, SubjectId = 2 }
            );

            
            modelBuilder.Entity<StudentProfile>().HasData(
                new StudentProfile { Id = 1, StudentId = 1, Address = "Cairo", DateOfBirth = new DateTime(2005, 1, 1) },
                new StudentProfile { Id = 2, StudentId = 2, Address = "Alex", DateOfBirth = new DateTime(2004, 2, 2) },
                new StudentProfile { Id = 3, StudentId = 3, Address = "Giza", DateOfBirth = new DateTime(2003, 3, 3) },
                new StudentProfile { Id = 4, StudentId = 4, Address = "Sharqia", DateOfBirth = new DateTime(2002, 4, 4) }
            );


        }
        }

    }
