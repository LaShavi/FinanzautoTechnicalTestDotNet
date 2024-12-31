namespace TechnicalTestDotNet.Core.DTOs.Auth
{
    public class CreateUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ResponseCreateUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
