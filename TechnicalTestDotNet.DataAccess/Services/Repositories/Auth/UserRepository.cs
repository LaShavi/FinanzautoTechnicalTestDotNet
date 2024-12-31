using Microsoft.EntityFrameworkCore;
using System.Net;
using TechnicalTestDotNet.Core.DTOs;
using TechnicalTestDotNet.Core.DTOs.Auth;
using TechnicalTestDotNet.Core.Helpers.CustomHttpExceptions;
using TechnicalTestDotNet.Core.Interfaces.Auth;
using TechnicalTestDotNet.Core.Interfaces.Utilities.Encrypt;
using TechnicalTestDotNet.Core.Models;
using TechnicalTestDotNet.DataAccess.DataBase;
using TechnicalTestDotNet.DataAccess.DataBase.Models;
using TechnicalTestDotNet.DataAccess.Helpers;

namespace TechnicalTestDotNet.DataAccess.Services.Repositories.Auth
{
    public class UserRepository : IUserRepository
    {
        private readonly dbContext _dbContext;
        Utiles _util = new Utiles();

        public UserRepository(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private async Task<bool> ExistsUserByEmail(string email)
        {
            if (await _dbContext.User.Where(x => x.Email.Equals(email)).FirstOrDefaultAsync() is null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ValidateUserAsync(LoginRequest loginRequest)
        {
            loginRequest.Password = EncryptPasswork.GetSHA256(loginRequest.Password);

            var response = await _dbContext.User.Where(x => x.Email.Equals(loginRequest.Username) && x.Password.Equals(loginRequest.Password)).FirstOrDefaultAsync();
            if (response == null)
            {
                return false;
            } 
            else
            {
                return true;
            }            
        }

        public async Task<ResponseCreateUserDTO> CreateUser(CreateUserDTO input)
        {

            // Validamos si el usuario ya ha sido creado.
            if (await ExistsUserByEmail(input.Email) == true)
            {
                throw new CustomHttpException(new ErrorDetails
                {
                    MessageUser = "El usuario YA Ha sido registrado.",
                    StatusCode = HttpStatusCode.Conflict,
                    Messages = new List<string> { "Userio ya registrado" }
                });
            }

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Generamos una clave automatica.                    
                    string PasswordEncrypt = EncryptPasswork.GetSHA256(input.Password);


                    #region TEMP
                    // Armamos objeto para insertar
                    User user = new User();
                    user.Name = input.Name;
                    user.Email = input.Email;
                    user.Password = PasswordEncrypt;

                    // Guardamos usuario
                    _dbContext.Add(user);
                    await _dbContext.SaveChangesAsync();

                    // Commit de la transacción
                    transaction.Commit();
                    #endregion

                    // Regresamos datos
                    return new ResponseCreateUserDTO
                    {
                        Name = user.Name,
                        Email = user.Email,                        
                    };
                }
                catch (Exception ex)
                {
                    // En caso de error, hacemos un rollback de la transacción
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
