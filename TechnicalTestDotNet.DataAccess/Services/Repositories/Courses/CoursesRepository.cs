using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechnicalTestDotNet.Core.DTOs.Courses;
using TechnicalTestDotNet.Core.DTOs;
using TechnicalTestDotNet.Core.Interfaces.Courses;
using TechnicalTestDotNet.DataAccess.DataBase;
using TechnicalTestDotNet.DataAccess.DataBase.Models;
using TechnicalTestDotNet.Core.Extensions;

namespace TechnicalTestDotNet.DataAccess.Services.Repositories.Courses
{
    public class CoursesRepository: ICoursesRepository
    {
        #region Dependency Injection

        private readonly dbContext _dbContext;
        public IConfiguration Configuration { get; }
        private readonly IMapper _mapper;
        Utils _util = new Utils();

        public CoursesRepository(dbContext dbContext, IConfiguration configuration, IMapper mapper)
        {
            _dbContext = dbContext;
            Configuration = configuration;
            _mapper = mapper;
        }
        #endregion

        // Metodos


        /// <summary>
        /// Consulta Todos los Cursos
        /// </summary>
        /// <returns>Registros</returns>
        public async Task<List<ResponseCourseDTO>> GetCourses()
        {
            var data = await _dbContext.Course.Select(x => new ResponseCourseDTO
            {
                Id = x.Id,
                Name = x.Name,
                UserCreated = x.UserCreated,
                DateCreated = x.DateCreated
            }).ToListAsync();

            data.ObjectIfIsNotNull("No se encontraron registros.");

            return data;
        }

        /// <summary>
        /// Consulta Curso, por Id
        /// </summary>
        /// <returns>Registros</returns>
        public async Task<ResponseCourseDTO> GetCoursesById(int Id)
        {
            var data = await _dbContext.Course.Select(x => new ResponseCourseDTO
            {
                Id = x.Id,
                Name = x.Name,
                UserCreated = x.UserCreated,
                DateCreated = x.DateCreated
            })
            .Where(x => x.Id == Id)
            .FirstOrDefaultAsync();

            data.ObjectIfIsNotNull("No se encontraron registros.");

            return data;
        }

        /// <summary>
        /// Creamos un nuevo Curso
        /// </summary>
        /// <returns>Id del nuevo registro</returns>
        public async Task<LlaveValorDTO> AddCourse(InputCourseDTO input)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Mapeamos datos a insertar
                    var newRecord = new Course
                    {                        
                        Name = input.Name,                        
                        UserCreated = input.User,
                        DateCreated = DateTime.Now
                    };

                    #region Insert Registro                    
                    // Guarda los cambios en la base de datos.
                    await _dbContext.Course.AddAsync(newRecord);
                    await _dbContext.SaveChangesAsync();
                    var newId = newRecord.Id;
                    #endregion

                    // Confirma la transacción si todo fue exitoso
                    await transaction.CommitAsync();

                    // Creamos respuesta
                    var response = new LlaveValorDTO
                    {
                        Id = 1,
                        Valor = "Registrado correctamente, Id: " + newId
                    };

                    return response;
                }
                catch (Exception ex)
                {
                    // Realiza un rollback en caso de error
                    await transaction.RollbackAsync();

                    // Creamos respuesta
                    var response = new LlaveValorDTO
                    {
                        Id = -1,
                        Valor = "Ha ocurrido un error"
                    };

                    return response;
                }
            }
        }

        /// <summary>
        /// Editamos un Curso
        /// </summary>
        /// <returns>Id del registro</returns>
        public async Task<LlaveValorDTO> EditCourse(EditDTO<InputCourseDTO> input)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Consultamos registro a editar
                    var record = await _dbContext.Course.Where(x => x.Id == input.Id).FirstOrDefaultAsync();

                    // Mapeamos datos para actualizar                    
                    record.Name = input.Data.Name;                    

                    // Actualizar datos.
                    _dbContext.Course.Update(record);
                    await _dbContext.SaveChangesAsync();

                    // Confirma la transacción si todo fue exitoso
                    await transaction.CommitAsync();

                    // Creamos respuesta
                    var response = new LlaveValorDTO
                    {
                        Id = 1,
                        Valor = "Editado correctamente, Id: " + record.Id
                    };

                    return response;
                }
                catch (Exception ex)
                {
                    // Realiza un rollback en caso de error
                    await transaction.RollbackAsync();

                    // Creamos respuesta
                    var response = new LlaveValorDTO
                    {
                        Id = -1,
                        Valor = "Ha ocurrido un error"
                    };

                    return response;
                }
            }
        }

        /// <summary>
        /// Eliminamos un Curso
        /// </summary>
        /// <returns>Id del registro</returns>
        public async Task<LlaveValorDTO> DeleteCourse(int Id)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Consultamos registro a editar
                    var record = await _dbContext.Course.Where(x => x.Id == Id).FirstOrDefaultAsync();

                    // Eliminamos datos.
                    if (record != null)
                    {
                        _dbContext.Course.Remove(record);
                        await _dbContext.SaveChangesAsync();
                    }

                    // Confirma la transacción si todo fue exitoso
                    await transaction.CommitAsync();

                    // Creamos respuesta
                    var response = new LlaveValorDTO
                    {
                        Id = 1,
                        Valor = "Eliminado correctamente"
                    };

                    return response;
                }
                catch (Exception ex)
                {
                    // Realiza un rollback en caso de error
                    await transaction.RollbackAsync();

                    // Creamos respuesta
                    var response = new LlaveValorDTO
                    {
                        Id = -1,
                        Valor = "Ha ocurrido un error"
                    };

                    return response;
                }
            }
        }
    }
}

