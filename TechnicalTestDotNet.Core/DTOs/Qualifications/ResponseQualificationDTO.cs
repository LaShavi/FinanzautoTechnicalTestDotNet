namespace TechnicalTestDotNet.Core.DTOs.Qualifications
{
    public class ResponseQualificationDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public DateTime Date { get; set; }
        public int Value { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
