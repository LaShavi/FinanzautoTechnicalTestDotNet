using System.ComponentModel.DataAnnotations;

namespace TechnicalTestDotNet.Core.DTOs.Qualifications
{
    public class FilterQualificationDTO
    {        
        public int? StudentId { get; set; }

        public int? TeacherId { get; set; }

        public int? CourseId { get; set; }

        public DateTime? Date { get; set; }

        public int? Value { get; set; }
    }
}
