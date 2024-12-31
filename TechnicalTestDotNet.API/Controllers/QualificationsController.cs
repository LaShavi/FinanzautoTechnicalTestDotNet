using Microsoft.AspNetCore.Mvc;
using TechnicalTestDotNet.Core.DTOs.Qualifications;
using TechnicalTestDotNet.Core.DTOs;
using TechnicalTestDotNet.Core.Interfaces.Qualifications;
using Microsoft.AspNetCore.Authorization;

namespace TechnicalTestDotNet.API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController] // [ApiController, Authorize]
    [Authorize]
    [ApiController]
    public class QualificationsController : ControllerBase
    {
        #region Dependency Injection
        private readonly IQualificationsRepository _IRepository;
        public QualificationsController(IQualificationsRepository IQualificationsRepository)
        {
            _IRepository = IQualificationsRepository;
        }
        #endregion

        // Servicios

        /// <summary>
        /// Consulta Todos los Calificaciones
        /// </summary>
        /// <returns>Registros</returns>
        [HttpGet]
        [Route("GetQualifications")]
        public async Task<ActionResult<ResponseQualificationDTO>> GetQualifications() => Ok(await _IRepository.GetQualifications());

        /// <summary>
        /// Consulta Calificacion, por Id
        /// </summary>
        /// <returns>Registros</returns>
        [HttpGet]
        [Route("GetQualificationsById")]
        public async Task<ActionResult<ResponseQualificationDTO>> GetQualificationsById(int Id) => Ok(await _IRepository.GetQualificationsById(Id));

        /// <summary>
        /// Creamos un nuevo Calificacion
        /// </summary>
        /// <returns>Id del nuevo registro</returns>
        [HttpPost]
        [Route("AddQualification")]
        public async Task<ActionResult<LlaveValorDTO>> AddQualification(InputQualificationDTO input) => Ok(await _IRepository.AddQualification(input));

        /// <summary>
        /// Editamos un Calificacion
        /// </summary>
        /// <returns>Id del nuevo registro</returns>
        [HttpPut]
        [Route("EditQualification")]
        public async Task<ActionResult<LlaveValorDTO>> EditQualification(EditDTO<InputQualificationDTO> input) => Ok(await _IRepository.EditQualification(input));

        /// <summary>
        /// Eliminamos un Calificacion
        /// </summary>
        /// <returns>Id del registro</returns>
        [HttpDelete]
        [Route("DeleteQualification")]
        public async Task<ActionResult<LlaveValorDTO>> DeleteQualification(int Id) => Ok(await _IRepository.DeleteQualification(Id));
    }
}
