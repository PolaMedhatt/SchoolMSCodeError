namespace SchoolManagementSystem.DTOs.TeacherDTOs
{
    public class TeacherUpdateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
        public List<int> ClassIds { get; set; }
    }
}
