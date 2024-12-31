using System.ComponentModel.DataAnnotations;

namespace TechnicalTestDotNet.Core.DTOs.Qualifications
{
    public class EditQualificationDTO
    {
        [Required(ErrorMessage = "El campo 'Id' es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo 'Id del Estudiante' es obligatorio.")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "El campo 'Id del Profesor' es obligatorio.")]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "El campo 'Id del Curso' es obligatorio.")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "El campo 'Fecha' es obligatorio.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "El campo 'Valor' es obligatorio.")]
        public int Value { get; set; }

        [Required(ErrorMessage = "El campo 'Usuario' es obligatorio.")]
        [StringLength(100)]
        public string User { get; set; }
    }
}
