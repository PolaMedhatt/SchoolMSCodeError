namespace SchoolManagementSystem.DTOs.TeacherDTOs
{
    public class TeacherReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
        public List<string> Classes { get; set; }
    }
}
