using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public List<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
    }

}
