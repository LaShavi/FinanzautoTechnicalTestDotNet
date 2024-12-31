using System.ComponentModel.DataAnnotations;

namespace TechnicalTestDotNet.Core.DTOs.Teachers
{
    public class EditTeacherDTO
    {
        [Required(ErrorMessage = "El campo 'Id' es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo 'Identificacion' es obligatorio.")]
        [StringLength(10, ErrorMessage = "La longitud máxima para el campo 'Identificacion' es de 10 caracteres.")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "El campo 'Nombre' es obligatorio.")]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo 'Correo electrónico' es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo 'Correo electrónico' no es una dirección de correo electrónico válida.")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo 'Fecha de nacimiento' es obligatorio.")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "El campo 'Nivel de educación' es obligatorio.")]
        public int EducationLevel { get; set; }

        [Required(ErrorMessage = "El campo 'Usuario' es obligatorio.")]
        [StringLength(100)]
        public string User { get; set; }
    }
}
