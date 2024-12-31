using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechnicalTestDotNet.Core.DTOs.Qualifications;
using TechnicalTestDotNet.Core.DTOs;
using TechnicalTestDotNet.Core.Interfaces.Qualifications;
using TechnicalTestDotNet.DataAccess.DataBase;
using TechnicalTestDotNet.DataAccess.DataBase.Models;
using TechnicalTestDotNet.Core.Extensions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json.Linq;

namespace TechnicalTestDotNet.DataAccess.Services.Repositories.Qualifications
{
    public class QualificationsRepository: IQualificationsRepository
    {
        #region Dependency Injection

        private readonly dbContext _dbContext;
        public IConfiguration Configuration { get; }
        private readonly IMapper _mapper;
        Utils _util = new Utils();

        public QualificationsRepository(dbContext dbContext, IConfiguration configuration, IMapper mapper)
        {
            _dbContext = dbContext;
            Configuration = configuration;
            _mapper = mapper;
        }
        #endregion

        /// <summary>
        /// Consulta Todos los Calificaciones
        /// </summary>
        /// <returns>Registros</returns>
        public async Task<List<ResponseQualificationDTO>> GetQualifications()
        {
            var data = await _dbContext.Qualification.Select(x => new ResponseQualificationDTO
            {
                Id = x.Id,
                StudentId = x.StudentId,
                TeacherId = x.TeacherId,
                CourseId = x.CourseId,
                Date = x.Date,
                Value = x.Value,
                UserCreated = x.UserCreated,
                DateCreated = x.DateCreated
            }).ToListAsync();

            data.ObjectIfIsNotNull("No se encontraron registros.");

            return data;
        }

        /// <summary>
        /// Consulta Calificacion, por Id
        /// </summary>
        /// <returns>Registros</returns>
        public async Task<ResponseQualificationDTO> GetQualificationsById(int Id)
        {
            var data = await _dbContext.Qualification.Select(x => new ResponseQualificationDTO
            {
                Id = x.Id,
                StudentId = x.StudentId,
                TeacherId = x.TeacherId,
                CourseId = x.CourseId,
                Date = x.Date,
                Value = x.Value,
                UserCreated = x.UserCreated,
                DateCreated = x.DateCreated
            })
            .Where(x => x.Id == Id)
            .FirstOrDefaultAsync();

            data.ObjectIfIsNotNull("No se encontraron registros.");

            return data;
        }

        /// <summary>
        /// Creamos un nuevo Calificacion
        /// </summary>
        /// <returns>Id del nuevo registro</returns>
        public async Task<LlaveValorDTO> AddQualification(InputQualificationDTO input)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Mapeamos datos a insertar
                    var newRecord = new Qualification
                    {
                        StudentId = input.StudentId,
                        TeacherId = input   .TeacherId,
                        CourseId = input.CourseId,
                        Date = input.Date,
                        Value = input.Value,
                        UserCreated = input.User,
                        DateCreated = DateTime.Now
                    };

                    #region Insert Registro                    
                    // Guarda los cambios en la base de datos.
                    await _dbContext.Qualification.AddAsync(newRecord);
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
        /// Editamos un Calificacion
        /// </summary>
        /// <returns>Id del registro</returns>
        public async Task<LlaveValorDTO> EditQualification(EditDTO<InputQualificationDTO> input)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Consultamos registro a editar
                    var record = await _dbContext.Qualification.Where(x => x.Id == input.Id).FirstOrDefaultAsync();

                    // Mapeamos datos para actualizar
                    record.StudentId = input.Data.StudentId;
                    record.TeacherId = input.Data.TeacherId;
                    record.CourseId = input.Data.CourseId;
                    record.Date = input.Data.Date;
                    record.Value = input.Data.Value;

                    // Actualizar datos.
                    _dbContext.Qualification.Update(record);
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
        /// Eliminamos un Calificacion
        /// </summary>
        /// <returns>Id del registro</returns>
        public async Task<LlaveValorDTO> DeleteQualification(int Id)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Consultamos registro a editar
                    var record = await _dbContext.Qualification.Where(x => x.Id == Id).FirstOrDefaultAsync();

                    // Eliminamos datos.
                    if (record != null)
                    {
                        _dbContext.Qualification.Remove(record);
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
