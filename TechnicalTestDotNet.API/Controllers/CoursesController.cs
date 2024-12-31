using Microsoft.AspNetCore.Mvc;
using TechnicalTestDotNet.Core.DTOs;
using TechnicalTestDotNet.Core.Interfaces.Courses;
using TechnicalTestDotNet.Core.DTOs.Courses;
using Microsoft.AspNetCore.Authorization;

namespace TechnicalTestDotNet.API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController] // [ApiController, Authorize]
    [Authorize]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        #region Dependency Injection
        private readonly ICoursesRepository _IRepository;
        public CoursesController(ICoursesRepository ICoursesRepository)
        {
            _IRepository = ICoursesRepository;
        }
        #endregion

        // Servicios

        /// <summary>
        /// Consulta Todos los Cursos
        /// </summary>
        /// <returns>Registros</returns>
        [HttpGet]
        [Route("GetCourses")]
        public async Task<ActionResult<ResponseCourseDTO>> GetCourses() => Ok(await _IRepository.GetCourses());

        /// <summary>
        /// Consulta Curso, por Id
        /// </summary>
        /// <returns>Registros</returns>
        [HttpGet]
        [Route("GetCoursesById")]
        public async Task<ActionResult<ResponseCourseDTO>> GetCoursesById(int Id) => Ok(await _IRepository.GetCoursesById(Id));

        /// <summary>
        /// Creamos un nuevo Curso
        /// </summary>
        /// <returns>Id del nuevo registro</returns>
        [HttpPost]
        [Route("AddCourse")]
        public async Task<ActionResult<LlaveValorDTO>> AddCourse(InputCourseDTO input) => Ok(await _IRepository.AddCourse(input));

        /// <summary>
        /// Editamos un Curso
        /// </summary>
        /// <returns>Id del nuevo registro</returns>
        [HttpPut]
        [Route("EditCourse")]
        public async Task<ActionResult<LlaveValorDTO>> EditCourse(EditDTO<InputCourseDTO> input) => Ok(await _IRepository.EditCourse(input));

        /// <summary>
        /// Eliminamos un Curso
        /// </summary>
        /// <returns>Id del registro</returns>
        [HttpDelete]
        [Route("DeleteCourse")]
        public async Task<ActionResult<LlaveValorDTO>> DeleteCourse(int Id) => Ok(await _IRepository.DeleteCourse(Id));
    }
}
