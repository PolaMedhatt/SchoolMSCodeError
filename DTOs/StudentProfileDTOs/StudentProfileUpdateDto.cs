using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.DTOs.StudentProfileDTOs
{
    public class StudentProfileUpdateDto
    {
        [Required, MaxLength(100)] public string Address { get; set; }
        [Required] public DateTime DateOfBirth { get; set; }
    }
}
