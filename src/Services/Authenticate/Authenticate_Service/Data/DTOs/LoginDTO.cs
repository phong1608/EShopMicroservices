using System.ComponentModel.DataAnnotations;

namespace Authenticate.API.Data.DTOs
{
    public class LoginDTO
    {
        public string? Email { get; set; }

        public string? Password { get; set; }
        public LoginDTO(string Email,string Password)
        {
            this.Email = Email;
            this.Password = Password;
        }
        public LoginDTO() { }
    }
}
