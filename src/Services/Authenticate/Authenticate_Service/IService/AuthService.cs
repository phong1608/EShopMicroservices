using Authenticate.API.Data.DTOs;
using Authenticate.API.Models;
using Authenticate_Service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Authenticate.API.IService
{
    public class AuthService : IAuthService
    {
       
            private readonly UserContext _db;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly RoleManager<ApplicationRole> _roleManager;
            private readonly IJwtTokenGenerator _jwtTokenGenerator;

            public AuthService(UserContext db, IJwtTokenGenerator jwtTokenGenerator,
                UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
            {
                _db = db;
                _jwtTokenGenerator = jwtTokenGenerator;
                _userManager = userManager;
                _roleManager = roleManager;
            }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(nameof(email) + " or " + nameof(roleName));
            }

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email!.ToLower() == email.ToLower());
            if (user == null)
            {
                return false; 
            }

            var existingRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (existingRole == null)
            {
                await _roleManager.CreateAsync(new ApplicationRole { Name = roleName });
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            await _userManager.AddToRoleAsync(user, roleName);

            return true;
        }

        public async Task<string> Login(LoginDTO loginDTO)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email!.ToLower() == loginDTO.Email!.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user!, loginDTO.Password!);

            if (user == null || isValid == false)
            {
                throw new Exception();
            }

            //if user was found , Generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.CreateJwtToken(user, roles.First());

               

            return token.ToString();
        }

            public async Task<Guid> Register(RegisterDTO RegisterDTO)
            {
                ApplicationUser user = new()
                {
                    UserName = RegisterDTO.Email,
                    Email = RegisterDTO.Email,
                    NormalizedEmail = RegisterDTO.Email!.ToUpper(),
                    Name = RegisterDTO.Name,
                };
                var result = await _userManager.CreateAsync(user, RegisterDTO.Password!);
                if (result.Succeeded)
                {
                    var newUser = _db.Users.First(u => u.Email == RegisterDTO.Email);
                    if (newUser == null)
                        throw new Exception();

                    await _userManager.AddToRoleAsync(newUser, "Customer");
                    return newUser.Id;

                }
                else
                {
                throw new Exception();
                }
                
               
            }
        }
    
}
