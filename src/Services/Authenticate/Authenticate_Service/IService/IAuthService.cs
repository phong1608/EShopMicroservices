using Authenticate.API.Data.DTOs;

namespace Authenticate.API.IService
{
    public interface IAuthService
    {
        Task<Guid> Register(RegisterDTO registrationDTO);
        Task<string> Login(LoginDTO loginDTO);
        Task<bool> AssignRole(string email, string roleName);
    }
}
