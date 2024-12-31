using System.ComponentModel.DataAnnotations;

namespace TechnicalTestDotNet.Core.DTOs.Teachers
{
    public class FilterTeacherDTO
    {
        public string? IdentificationNumber { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public DateTime? Birthday { get; set; }

        public int? EducationLevel { get; set; }
    }
}
