using Authenticate_Service.Models;

namespace Authenticate.API.IService
{
    public interface IJwtTokenGenerator
    {
        string CreateJwtToken(ApplicationUser user,string role);
    }
}
