using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechnicalTestDotNet.Core.Interfaces;
using TechnicalTestDotNet.Core.Models;

namespace TechnicalTestDotNet.Core.Helpers.Token
{
    #region V1 Obsoleto
    //public class TokenService : ITokenService
    //{

    //    private readonly IConfiguration _configuration;

    //    public TokenService(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }

    //    public UserToken BuildToken(User user)
    //    {
    //        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
    //        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

    //        var claims = new List<Claim>
    //        {
    //            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
    //            new Claim(ClaimTypes.Name, user.FullName),
    //            new Claim("User", user.UserName),
    //        };            

    //        var tiempoExpiracion = DateTime.Now.AddMinutes(double.Parse(_configuration["Authentication:TiempoExpiracion"])); // 480

    //        var token = new JwtSecurityTokenHandler().CreateJwtSecurityToken(
    //            issuer: _configuration["Authentication:Issuer"],
    //            audience: _configuration["Authentication:Audience"],
    //            subject: new ClaimsIdentity(claims),
    //            notBefore: DateTime.Now,
    //            expires: tiempoExpiracion,
    //            issuedAt: null,
    //            signingCredentials: signingCredentials
    //            );

    //        return new UserToken()
    //        {
    //            ExpirationToken = tiempoExpiracion,
    //            Token = new JwtSecurityTokenHandler().WriteToken(token)
    //        };
    //    }
    //}
    #endregion

    #region V2 Actual
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string username)
        {
            var jwtKey = _configuration["Authentication:SecretKey"];
            var jwtIssuer = _configuration["Authentication:Issuer"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtIssuer,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    #endregion
}
