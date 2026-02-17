using SchoolManagementSystem.DTOs.StudentProfileDTOs;

namespace SchoolManagementSystem.DTOs.StudentDTOS
{
    public class StudentReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ?ClassName { get; set; }
        public List<string> Subjects { get; set; }
        public StudentProfileReadDto Profile { get; set; }
    }
}
