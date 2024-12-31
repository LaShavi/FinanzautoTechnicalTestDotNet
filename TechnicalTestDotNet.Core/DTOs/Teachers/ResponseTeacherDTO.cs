namespace TechnicalTestDotNet.Core.DTOs.Teachers
{
    public class ResponseTeacherDTO
    {
        public int Id { get; set; }
        public string IdentificationNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public int EducationLevel { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
