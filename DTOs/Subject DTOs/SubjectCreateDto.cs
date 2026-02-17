namespace SchoolManagementSystem.DTOs.Subject_DTOs
{
    public class SubjectCreateDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public List<int> StudentIds { get; set; } 
    }

}
