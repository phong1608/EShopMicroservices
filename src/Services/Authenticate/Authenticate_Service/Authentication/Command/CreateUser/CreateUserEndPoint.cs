using Authenticate.API.Data.DTOs;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Authenticate.API.Authentication.Command.CreateUser
{
    public record CreateUserRequest(RegisterDTO RegisterDTO);
    public record CreateUserResponse(Guid UserId);
    public class CreateUserEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/user/register/", async (CreateUserRequest request, ISender sender) =>
            {
                var command = new CreateUserCommand(request.RegisterDTO);
                var result = await sender.Send(command);
                var response = new CreateUserResponse(result.UserId);
                return Results.Ok(response);
            });
        }
    }
}
