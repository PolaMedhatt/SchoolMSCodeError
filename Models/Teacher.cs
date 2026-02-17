using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SchoolManagementSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(50)]
        public string Specialization { get; set; }

        public List<Class> Classes { get; set; } = new List<Class>();
    }

}
