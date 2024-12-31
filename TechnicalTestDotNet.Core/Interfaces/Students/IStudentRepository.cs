using TechnicalTestDotNet.Core.DTOs;
using TechnicalTestDotNet.Core.DTOs.Students;

namespace TechnicalTestDotNet.Core.Interfaces.Students
{
    public interface IStudentRepository
    {
        /// <summary>
        /// Consulta Todos los Estudiantes, paginando y filtrando
        /// </summary>
        /// <returns>Registros</returns>
        Task<List<ResponseStudentDTO>> GetStudentsFilter(InputPaginateDTO<FilterStudentDTO> input);

        /// <summary>
        /// Consulta Todos los Estudiantes
        /// </summary>
        /// <returns>Registros</returns>
        Task<List<ResponseStudentDTO>> GetStudents();

        /// <summary>
        /// Consulta un Estudiante, segun Identificacion
        /// </summary>
        /// <returns>Registro</returns>
        Task<ResponseStudentDTO> GetStudentByIdentification(string IdentificationNumber);

        /// <summary>
        /// Creamos un nuevo Estudiante
        /// </summary>
        /// <returns>Id del nuevo registro</returns>
        Task<LlaveValorDTO> AddStudent(InputStudentDTO input);

        /// <summary>
        /// Editamos un Estudiante
        /// </summary>
        /// <returns>Id del registro</returns>
        Task<LlaveValorDTO> EditStudent(EditDTO<InputStudentDTO> input);

        /// <summary>
        /// Eliminamos un Estudiante
        /// </summary>
        /// <returns>Id del registro</returns>
        Task<LlaveValorDTO> DeleteStudent(int Id);
    }
}
