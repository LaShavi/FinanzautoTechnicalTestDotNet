using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechnicalTestDotNet.Core.DTOs;
using TechnicalTestDotNet.Core.DTOs.Teachers;
using TechnicalTestDotNet.Core.Extensions;
using TechnicalTestDotNet.Core.Interfaces.Teachers;
using TechnicalTestDotNet.DataAccess.DataBase;
using TechnicalTestDotNet.DataAccess.DataBase.Models;

namespace TechnicalTestDotNet.DataAccess.Services.Repositories.Teachers
{
    public class TeachersRepository: ITeachersRepository
    {
        #region Dependency Injection

        private readonly dbContext _dbContext;
        public IConfiguration Configuration { get; }
        private readonly IMapper _mapper;
        Utils _util = new Utils();

        public TeachersRepository(dbContext dbContext, IConfiguration configuration, IMapper mapper)
        {
            _dbContext = dbContext;
            Configuration = configuration;
            _mapper = mapper;
        }
        #endregion


        // Metodos

        /// <summary>
        /// Consulta Todos los Profesores, paginando y filtrando
        /// </summary>
        /// <returns>Registros</returns>
        public async Task<List<ResponseTeacherDTO>> GetTeachersFilter(InputPaginateDTO<FilterTeacherDTO> input)
        {
            // Calcular el índice de la página
            int pageIndex = input.pageIndex - 1;

            // Calcular la cantidad de registros para omitir
            int skip = pageIndex * input.pageSize;

            // Consulta base
            var query = _dbContext.Teacher.AsQueryable();

            // Aplicar filtros del DTO FilterTeacherDTO
            if (input.Query != null)
            {
                if (!string.IsNullOrEmpty(input.Query.IdentificationNumber))
                {
                    query = query.Where(x => x.IdentificationNumber == input.Query.IdentificationNumber);
                }

                if (!string.IsNullOrEmpty(input.Query.Name))
                {
                    query = query.Where(x => x.Name == input.Query.Name);
                }

                if (!string.IsNullOrEmpty(input.Query.LastName))
                {
                    query = query.Where(x => x.LastName == input.Query.LastName);
                }

                if (!string.IsNullOrEmpty(input.Query.Email))
                {
                    query = query.Where(x => x.Email == input.Query.Email);
                }

                if (input.Query.Birthday != null)
                {
                    query = query.Where(x => x.Birthday == input.Query.Birthday);
                }

                if (input.Query.EducationLevel != null)
                {
                    query = query.Where(x => x.EducationLevel == input.Query.EducationLevel);
                }
            }

            // Aplicar paginación
            var data = await query
                .Skip(skip)
                .Take(input.pageSize)
                .Select(x => new ResponseTeacherDTO
                {
                    Id = x.Id,
                    IdentificationNumber = x.IdentificationNumber,
                    Name = x.Name,
                    LastName = x.LastName,
                    Email = x.Email,
                    Birthday = x.Birthday,
                    EducationLevel = x.EducationLevel,
                    UserCreated = x.UserCreated,
                    DateCreated = x.DateCreated
                })
                .ToListAsync();

            data.ObjectIfIsNotNull("No se encontraron registros.");

            return data;
        }


        /// <summary>
        /// Consulta Todos los Profesores
        /// </summary>
        /// <returns>Registros</returns>
        public async Task<List<ResponseTeacherDTO>> GetTeachers()
        {
            var data = await _dbContext.Teacher.Select(x => new ResponseTeacherDTO
            {
                Id = x.Id,
                IdentificationNumber = x.IdentificationNumber,
                Name = x.Name,
                LastName = x.LastName,
                Email = x.Email,
                Birthday = x.Birthday,
                EducationLevel = x.EducationLevel,
                UserCreated = x.UserCreated,
                DateCreated = x.DateCreated
            }).ToListAsync();

            data.ObjectIfIsNotNull("No se encontraron registros.");

            return data;
        }

        /// <summary>
        /// Consulta un Profesor, segun Identificacion
        /// </summary>
        /// <returns>Registro</returns>
        public async Task<ResponseTeacherDTO> GetTeacherByIdentification(string IdentificationNumber)
        {
            var data = await _dbContext.Teacher.Select(x => new ResponseTeacherDTO
            {
                Id = x.Id,
                IdentificationNumber = x.IdentificationNumber,
                Name = x.Name,
                LastName = x.LastName,
                Email = x.Email,
                Birthday = x.Birthday,
                EducationLevel = x.EducationLevel,
                UserCreated = x.UserCreated,
                DateCreated = x.DateCreated
            })
            .Where(x => x.IdentificationNumber == IdentificationNumber)
            .FirstOrDefaultAsync();

            data.ObjectIfIsNotNull("El registro no existe.");

            return data;
        }

        /// <summary>
        /// Creamos un nuevo Profesor
        /// </summary>
        /// <returns>Id del nuevo registro</returns>
        public async Task<LlaveValorDTO> AddTeacher(InputTeacherDTO input)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Mapeamos datos a insertar
                    var newRecord = new Teacher
                    {
                        IdentificationNumber = input.IdentificationNumber,
                        Name = input.Name,
                        LastName = input.LastName,
                        Email = input.Email,
                        Birthday = input.Birthday,
                        EducationLevel = input.EducationLevel,
                        UserCreated = input.User,
                        DateCreated = DateTime.Now
                    };                   

                    #region Insert Registro                    
                    // Guarda los cambios en la base de datos.
                    await _dbContext.Teacher.AddAsync(newRecord);
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
        /// Editamos un Profesor
        /// </summary>
        /// <returns>Id del registro</returns>
        public async Task<LlaveValorDTO> EditTeacher(EditDTO<InputTeacherDTO> input)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Consultamos registro a editar
                    var record = await _dbContext.Teacher.Where(x => x.Id == input.Id).FirstOrDefaultAsync();

                    // Mapeamos datos para actualizar
                    record.IdentificationNumber = input.Data.IdentificationNumber;
                    record.Name = input.Data.Name;
                    record.LastName = input.Data.LastName;
                    record.Email = input.Data.Email;
                    record.Birthday = input.Data.Birthday;
                    record.EducationLevel = input.Data.EducationLevel;

                    // Actualizar datos.
                    _dbContext.Teacher.Update(record);
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
        /// Eliminamos un Profesor
        /// </summary>
        /// <returns>Id del registro</returns>
        public async Task<LlaveValorDTO> DeleteTeacher(int Id)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Consultamos registro a editar
                    var record = await _dbContext.Teacher.Where(x => x.Id == Id).FirstOrDefaultAsync();

                    // Eliminamos datos.
                    if (record != null)
                    {
                        _dbContext.Teacher.Remove(record);
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
