namespace SchoolManagementSystem.DTOs.ClassDTOS
{
    public class ClassReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Grade { get; set; }
        public int Capacity { get; set; }
        public int RemainingSeats { get; set; }
        public string TeacherName { get; set; }
        public List<string> Students { get; set; }
    }

}
