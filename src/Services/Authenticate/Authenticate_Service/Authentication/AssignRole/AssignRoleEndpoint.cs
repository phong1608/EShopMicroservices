using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Authenticate.API.Authentication.AssignRole
{
    public record AssignRoleRequest(string Email, string RoleName);
    public record AssignRoleResponse(bool IsSuccess);
    public class AssignRoleEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/user/role", async (AssignRoleRequest request, ISender sender) =>
            {
                var command = request.Adapt<AssignRoleCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<AssignRoleResponse>();
                return Results.Ok(response);
            });
        }
    }
}
