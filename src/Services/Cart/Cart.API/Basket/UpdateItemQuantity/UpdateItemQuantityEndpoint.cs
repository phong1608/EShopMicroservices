
using Mapster;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cart.API.Basket.UpdateItemQuantity
{
    
    public record UpdateItemQuantityRequest(int Quantity);
    public record UpdateItemQuantityResponse(bool IsSuccess);
    public class UpdateItemQuantityEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/cart/items/{ProductId}", [Authorize] async(ISender sender, ClaimsPrincipal user,Guid ProductId,UpdateItemQuantityRequest request) =>
            {
                string userId = user.FindFirst(ClaimTypes.NameIdentifier!)!.Value;

                var command = new UpdateItemQuantityCommand(new Guid(userId),ProductId, request.Quantity);
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateItemQuantityResponse>();
                return Results.Ok(response);
            });
        }
    }
}
