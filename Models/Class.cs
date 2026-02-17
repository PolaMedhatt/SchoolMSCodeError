using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Class
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Grade { get; set; }

        [Range(1, int.MaxValue)]
        public int Capacity { get; set; }

        public int ?TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
    }

}
