using TechnicalTestDotNet.Core.DTOs;
using TechnicalTestDotNet.Core.DTOs.Courses;

namespace TechnicalTestDotNet.Core.Interfaces.Courses
{
    public interface ICoursesRepository
    {
        /// <summary>
        /// Consulta Todos los Cursos
        /// </summary>
        /// <returns>Registros</returns>
        Task<List<ResponseCourseDTO>> GetCourses();

        /// <summary>
        /// Consulta Curso, por Id
        /// </summary>
        /// <returns>Registros</returns>
        Task<ResponseCourseDTO> GetCoursesById(int Id);

        /// <summary>
        /// Creamos un nuevo Curso
        /// </summary>
        /// <returns>Id del nuevo registro</returns>
        Task<LlaveValorDTO> AddCourse(InputCourseDTO input);

        /// <summary>
        /// Editamos un Curso
        /// </summary>
        /// <returns>Id del registro</returns>
        Task<LlaveValorDTO> EditCourse(EditDTO<InputCourseDTO> input);

        /// <summary>
        /// Eliminamos un Curso
        /// </summary>
        /// <returns>Id del registro</returns>
        Task<LlaveValorDTO> DeleteCourse(int Id);
    }
}
