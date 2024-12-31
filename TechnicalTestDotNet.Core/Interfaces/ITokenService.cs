using TechnicalTestDotNet.Core.Models;

namespace TechnicalTestDotNet.Core.Interfaces
{
    public interface ITokenService
    {
        UserToken BuildToken(UserOLD user);
    }
}
