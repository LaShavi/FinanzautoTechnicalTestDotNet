using TechnicalTestDotNet.Core.DTOs;
using TechnicalTestDotNet.Core.DTOs.Teachers;

namespace TechnicalTestDotNet.Core.Interfaces.Teachers
{
    public interface ITeachersRepository
    {
        /// <summary>
        /// Consulta Todos los Profesores, paginando y filtrando
        /// </summary>
        /// <returns>Registros</returns>
        Task<List<ResponseTeacherDTO>> GetTeachersFilter(InputPaginateDTO<FilterTeacherDTO> input);

        /// <summary>
        /// Consulta Todos los Profesores
        /// </summary>
        /// <returns>Registros</returns>
        Task<List<ResponseTeacherDTO>> GetTeachers();

        /// <summary>
        /// Consulta un Profesor, segun Identificacion
        /// </summary>
        /// <returns>Registro</returns>
        Task<ResponseTeacherDTO> GetTeacherByIdentification(string IdentificationNumber);

        /// <summary>
        /// Creamos un nuevo Profesor
        /// </summary>
        /// <returns>Id del nuevo registro</returns>
        Task<LlaveValorDTO> AddTeacher(InputTeacherDTO input);

        /// <summary>
        /// Editamos un Profesor
        /// </summary>
        /// <returns>Id del registro</returns>
        Task<LlaveValorDTO> EditTeacher(EditDTO<InputTeacherDTO> input);

        /// <summary>
        /// Eliminamos un Profesor
        /// </summary>
        /// <returns>Id del registro</returns>
        Task<LlaveValorDTO> DeleteTeacher(int Id);
    }
}
