namespace SchoolManagementSystem.DTOs.Subject_DTOs
{
    public class SubjectReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public List<string> Students { get; set; }
    }

}
