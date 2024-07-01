using Authenticate.API.Data.DTOs;
using Carter;
using Mapster;
using MediatR;

namespace Authenticate.API.Authentication.Login
{
    public record LoginRequest(LoginDTO Login);
    public record LoginResponse(string Token);
    public class LoginEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/user/login", async (LoginRequest request, ISender sender) =>
            {
                var command = request.Adapt<LoginCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<LoginResponse>();
                return Results.Ok(response);
            });
        }
    }
}
