using System.ComponentModel.DataAnnotations;

namespace TechnicalTestDotNet.Core.DTOs
{
    public class EditDTO<T>
    {
        [Required(ErrorMessage = "El campo 'Id' es obligatorio.")]
        public int Id { get; set; }

        public T Data { get; set; }
    }
}
