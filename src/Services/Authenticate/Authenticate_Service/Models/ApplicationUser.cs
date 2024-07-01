using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Authenticate_Service.Models
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public Guid? RoleId { get; set; }

        public virtual ApplicationRole? Role { get; set; }
    }
}
