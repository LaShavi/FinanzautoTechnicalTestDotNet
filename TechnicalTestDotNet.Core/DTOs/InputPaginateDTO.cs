using System.ComponentModel.DataAnnotations;

namespace TechnicalTestDotNet.Core.DTOs
{
    public class InputPaginateDTO<T>
    {
        [Required(ErrorMessage = "El campo pageIndex es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo pageIndex debe ser mayor que 0.")]
        public int pageIndex { get; set; }

        [Required(ErrorMessage = "El campo pageSize es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo pageSize debe ser mayor que 0.")]
        public int pageSize { get; set; }

        public T Query { get; set; }
    }
}
