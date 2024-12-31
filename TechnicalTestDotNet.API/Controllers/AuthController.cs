using Microsoft.AspNetCore.Mvc;
using TechnicalTestDotNet.Core.DTOs.Auth;
using TechnicalTestDotNet.Core.Helpers.Token;
using TechnicalTestDotNet.DataAccess.Services.Repositories.Auth;

namespace TechnicalTestDotNet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;

        public AuthController(UserRepository userRepository, IConfiguration configuration, TokenService tokenService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest loginRequest)
        {
            var valid = await _userRepository.ValidateUserAsync(loginRequest);
            
            if (valid == true)
            {
                var token = _tokenService.GenerateToken(loginRequest.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized(new { Message = "Credenciales inválidas" });
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<List<ResponseCreateUserDTO>>> CreateUser(CreateUserDTO input) => Ok(await _userRepository.CreateUser(input));
    }
}
