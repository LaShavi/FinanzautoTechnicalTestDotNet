using TechnicalTestDotNet.Core.DTOs.Auth;

namespace TechnicalTestDotNet.Core.Interfaces.Auth
{
    public interface IUserRepository
    {
        Task<bool> ValidateUserAsync(LoginRequest loginRequest);

        Task<ResponseCreateUserDTO> CreateUser(CreateUserDTO input);
    }
}
