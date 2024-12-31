using TechnicalTestDotNet.Core.DTOs;
using TechnicalTestDotNet.Core.DTOs.Qualifications;

namespace TechnicalTestDotNet.Core.Interfaces.Qualifications
{
    public interface IQualificationsRepository
    {
        /// <summary>
        /// Consulta Todos los Calificaciones
        /// </summary>
        /// <returns>Registros</returns>
        Task<List<ResponseQualificationDTO>> GetQualifications();

        /// <summary>
        /// Consulta Calificacion, por Id
        /// </summary>
        /// <returns>Registros</returns>
        Task<ResponseQualificationDTO> GetQualificationsById(int Id);

        /// <summary>
        /// Creamos un nuevo Calificacion
        /// </summary>
        /// <returns>Id del nuevo registro</returns>
        Task<LlaveValorDTO> AddQualification(InputQualificationDTO input);

        /// <summary>
        /// Editamos un Calificacion
        /// </summary>
        /// <returns>Id del registro</returns>
        Task<LlaveValorDTO> EditQualification(EditDTO<InputQualificationDTO> input);

        /// <summary>
        /// Eliminamos un Calificacion
        /// </summary>
        /// <returns>Id del registro</returns>
        Task<LlaveValorDTO> DeleteQualification(int Id);
    }
}
