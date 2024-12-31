using Microsoft.AspNetCore.Mvc;
using TechnicalTestDotNet.Core.DTOs.Students;
using TechnicalTestDotNet.Core.DTOs;
using TechnicalTestDotNet.Core.Interfaces.Students;
using Microsoft.AspNetCore.Authorization;

namespace TechnicalTestDotNet.API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController] // [ApiController, Authorize]
    [Authorize]
    [ApiController]
    public class StudentController : ControllerBase
    {
        #region Dependency Injection
        private readonly IStudentRepository _IRepository;
        public StudentController(IStudentRepository IStudentRepository)
        {
            _IRepository = IStudentRepository;
        }
        #endregion


        // Servicios

        /// <summary>
        /// Consulta Todos los Estudiantes, paginando y filtrando
        /// </summary>
        /// <returns>Registros</returns>
        [HttpPost]
        [Route("GetStudentsFilter")]
        public async Task<ActionResult<ResponseStudentDTO>> GetStudentsFilter(InputPaginateDTO<FilterStudentDTO> input) => Ok(await _IRepository.GetStudentsFilter(input));

        /// <summary>
        /// Consulta Todos los Estudiantes
        /// </summary>
        /// <returns>Registros</returns>
        [HttpGet]
        [Route("GetStudents")]
        public async Task<ActionResult<ResponseStudentDTO>> GetStudents() => Ok(await _IRepository.GetStudents());

        /// <summary>
        /// Consulta un Estudiante, segun Identificacion
        /// </summary>
        /// <returns>Registro</returns>
        [HttpGet]
        [Route("GetStudentByIdentification")]
        public async Task<ActionResult<ResponseStudentDTO>> GetStudentByIdentification(string IdentificationNumber) => Ok(await _IRepository.GetStudentByIdentification(IdentificationNumber));

        /// <summary>
        /// Creamos un nuevo Estudiante
        /// </summary>
        /// <returns>Id del nuevo registro</returns>
        [HttpPost]
        [Route("AddStudent")]
        public async Task<ActionResult<LlaveValorDTO>> AddStudent(InputStudentDTO input) => Ok(await _IRepository.AddStudent(input));

        /// <summary>
        /// Editamos un Estudiante
        /// </summary>
        /// <returns>Id del registro</returns>
        [HttpPost]
        [Route("EditStudent")]
        public async Task<ActionResult<LlaveValorDTO>> EditStudent(EditDTO<InputStudentDTO> input) => Ok(await _IRepository.EditStudent(input));

        /// <summary>
        /// Eliminamos un Estudiante
        /// </summary>
        /// <returns>Id del registro</returns>
        [HttpPost]
        [Route("DeleteStudent")]
        public async Task<ActionResult<LlaveValorDTO>> DeleteStudent(int id) => Ok(await _IRepository.DeleteStudent(id));

    }
}
