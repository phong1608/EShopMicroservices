
namespace Authenticate.API.Data.DTOs
{
    public class RegisterDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmedPassword { get; set; }
        public RegisterDTO() { }
        
    }
}
