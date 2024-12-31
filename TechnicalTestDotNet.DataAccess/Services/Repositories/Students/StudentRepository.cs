using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechnicalTestDotNet.Core.DTOs.Students;
using TechnicalTestDotNet.Core.DTOs;
using TechnicalTestDotNet.Core.Interfaces.Students;
using TechnicalTestDotNet.DataAccess.DataBase;
using TechnicalTestDotNet.DataAccess.DataBase.Models;
using TechnicalTestDotNet.Core.Extensions;

namespace TechnicalTestDotNet.DataAccess.Services.Repositories.Students
{
    public class StudentRepository: IStudentRepository
    {
        #region Dependency Injection

        private readonly dbContext _dbContext;
        public IConfiguration Configuration { get; }
        private readonly IMapper _mapper;
        Utils _util = new Utils();

        public StudentRepository(dbContext dbContext, IConfiguration configuration, IMapper mapper)
        {
            _dbContext = dbContext;
            Configuration = configuration;
            _mapper = mapper;
        }
        #endregion


        // Metodos

        /// <summary>
        /// Consulta Todos los Estudiantes, paginando y filtrando
        /// </summary>
        /// <returns>Registros</returns>
        public async Task<List<ResponseStudentDTO>> GetStudentsFilter(InputPaginateDTO<FilterStudentDTO> input)
        {
            // Calcular el índice de la página
            int pageIndex = input.pageIndex - 1;

            // Calcular la cantidad de registros para omitir
            int skip = pageIndex * input.pageSize;

            // Consulta base
            var query = _dbContext.Student.AsQueryable();

            // Aplicar filtros del DTO FilterStudentDTO
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
            }

            // Aplicar paginación
            var data = await query
                .Skip(skip)
                .Take(input.pageSize)
                .Select(x => new ResponseStudentDTO
                {
                    Id = x.Id,
                    IdentificationNumber = x.IdentificationNumber,
                    Name = x.Name,
                    LastName = x.LastName,
                    Email = x.Email,
                    Birthday = x.Birthday,                    
                    UserCreated = x.UserCreated,
                    DateCreated = x.DateCreated
                })
                .ToListAsync();

            data.ObjectIfIsNotNull("No se encontraron registros.");

            return data;
        }


        /// <summary>
        /// Consulta Todos los Estudiantes
        /// </summary>
        /// <returns>Registros</returns>
        public async Task<List<ResponseStudentDTO>> GetStudents()
        {
            var data = await _dbContext.Student.Select(x => new ResponseStudentDTO
            {
                Id = x.Id,
                IdentificationNumber = x.IdentificationNumber,
                Name = x.Name,
                LastName = x.LastName,
                Email = x.Email,
                Birthday = x.Birthday,                
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
        public async Task<ResponseStudentDTO> GetStudentByIdentification(string IdentificationNumber)
        {
            var data = await _dbContext.Student.Select(x => new ResponseStudentDTO
            {
                Id = x.Id,
                IdentificationNumber = x.IdentificationNumber,
                Name = x.Name,
                LastName = x.LastName,
                Email = x.Email,
                Birthday = x.Birthday,                
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
        public async Task<LlaveValorDTO> AddStudent(InputStudentDTO input)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Mapeamos datos a insertar
                    var newRecord = new Student
                    {
                        IdentificationNumber = input.IdentificationNumber,
                        Name = input.Name,
                        LastName = input.LastName,
                        Email = input.Email,
                        Birthday = input.Birthday,                        
                        UserCreated = input.User,
                        DateCreated = DateTime.Now
                    };

                    #region Insert Registro                    
                    // Guarda los cambios en la base de datos.
                    await _dbContext.Student.AddAsync(newRecord);
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
        public async Task<LlaveValorDTO> EditStudent(EditDTO<InputStudentDTO> input)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Consultamos registro a editar
                    var record = await _dbContext.Student.Where(x => x.Id == input.Id).FirstOrDefaultAsync();

                    // Mapeamos datos para actualizar
                    record.IdentificationNumber = input.Data.IdentificationNumber;
                    record.Name = input.Data.Name;
                    record.LastName = input.Data.LastName;
                    record.Email = input.Data.Email;
                    record.Birthday = input.Data.Birthday;                    

                    // Actualizar datos.
                    _dbContext.Student.Update(record);
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
        public async Task<LlaveValorDTO> DeleteStudent(int Id)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Consultamos registro a editar
                    var record = await _dbContext.Student.Where(x => x.Id == Id).FirstOrDefaultAsync();

                    // Eliminamos datos.
                    if (record != null)
                    {
                        _dbContext.Student.Remove(record);
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
