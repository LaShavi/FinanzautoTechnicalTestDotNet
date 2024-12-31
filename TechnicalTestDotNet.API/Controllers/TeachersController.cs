using Microsoft.AspNetCore.Mvc;
using TechnicalTestDotNet.Core.DTOs;
using TechnicalTestDotNet.Core.DTOs.Teachers;
using TechnicalTestDotNet.Core.Interfaces.Teachers;
using Microsoft.AspNetCore.Authorization;

namespace TechnicalTestDotNet.API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController] // [ApiController, Authorize]
    [Authorize]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        #region Dependency Injection
        private readonly ITeachersRepository _IRepository;
        public TeachersController(ITeachersRepository ITeachersRepository)
        {
            _IRepository = ITeachersRepository;
        }
        #endregion

        // Servicios        

        /// <summary>
        /// Consulta Todos los Profesores, paginando y filtrando
        /// </summary>
        /// <returns>Registros</returns>
        [HttpPost]
        [Route("GetTeachersFilter")]
        public async Task<ActionResult<ResponseTeacherDTO>> GetTeachersFilter(InputPaginateDTO<FilterTeacherDTO> input) => Ok(await _IRepository.GetTeachersFilter(input));

        /// <summary>
        /// Consulta un Profesor, segun Identificacion
        /// </summary>
        /// <returns>Registro</returns>
        [HttpGet]
        [Route("GetTeachers")]
        public async Task<ActionResult<ResponseTeacherDTO>> GetTeachers() => Ok(await _IRepository.GetTeachers());

        /// <summary>
        /// Consulta Todos los Profesores
        /// </summary>
        /// <returns>Registros</returns>
        [HttpGet]
        [Route("GetTeacherByIdentification")]
        public async Task<ActionResult<ResponseTeacherDTO>> GetTeacherByIdentification(string IdentificationNumber) => Ok(await _IRepository.GetTeacherByIdentification(IdentificationNumber));

        /// <summary>
        /// Creamos un nuevo Profesor
        /// </summary>
        /// <returns>Id del nuevo registro</returns>
        [HttpPost]
        [Route("AddTeacher")]
        public async Task<ActionResult<ResponseTeacherDTO>> AddTeacher(InputTeacherDTO input) => Ok(await _IRepository.AddTeacher(input));

        /// <summary>
        /// Editamos un Profesor
        /// </summary>
        /// <returns>Id del nuevo registro</returns>
        [HttpPut]
        [Route("EditTeacher")]
        public async Task<ActionResult<ResponseTeacherDTO>> EditTeacher(EditDTO<InputTeacherDTO> input) => Ok(await _IRepository.EditTeacher(input));

        /// <summary>
        /// Eliminamos un Profesor
        /// </summary>
        /// <returns>Id del registro</returns>
        [HttpDelete]
        [Route("DeleteTeacher")]
        public async Task<ActionResult<ResponseTeacherDTO>> DeleteTeacher(int Id) => Ok(await _IRepository.DeleteTeacher(Id));
    }
}
