namespace TechnicalTestDotNet.Core.DTOs.Students
{
    public class ResponseStudentDTO
    {
        public int Id { get; set; }
        public string IdentificationNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
