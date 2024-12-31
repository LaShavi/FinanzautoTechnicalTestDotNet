using System.ComponentModel.DataAnnotations;

namespace TechnicalTestDotNet.Core.DTOs.Courses
{
    public class InputCourseDTO
    {        
        [Required(ErrorMessage = "El campo 'Nombre' es obligatorio.")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo 'Usuario' es obligatorio.")]
        [StringLength(100)]
        public string User { get; set; }
    }
}
