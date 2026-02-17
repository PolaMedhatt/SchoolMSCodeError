using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class StudentProfile
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }

}
