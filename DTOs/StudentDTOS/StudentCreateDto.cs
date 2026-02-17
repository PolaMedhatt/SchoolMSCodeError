using SchoolManagementSystem.DTOs.StudentProfileDTOs;

namespace SchoolManagementSystem.DTOs.StudentDTOS
{
    public class StudentCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int ClassId { get; set; }


        // profile data
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        //public StudentProfileCreateDto Profile { get; set; }
    }

}
