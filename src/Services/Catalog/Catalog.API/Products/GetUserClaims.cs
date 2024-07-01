using Carter;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Catalog.API.Products
{
    public class GetUserClaims : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapGet("/getuser", [Authorize] (ClaimsPrincipal user) =>
            {
                string userId=user.FindFirst(ClaimTypes.NameIdentifier!)!.Value;
                return userId;
            });
        }
    }
}
