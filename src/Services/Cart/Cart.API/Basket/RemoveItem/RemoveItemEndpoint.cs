
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace Cart.API.Basket.RemoveItem
{
    public record RemoveItemResponse(bool IsSuccess); 
    public class RemoveItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/cart/item/{ProductId}",[Authorize] async (ISender sender, ClaimsPrincipal user,Guid ProductId) =>
            {
                string userId = user.FindFirst(ClaimTypes.NameIdentifier!)!.Value;
                var command = new RemoveItemCommand(new Guid(userId),ProductId);
                var result = await sender.Send(command);
                var response = result.Adapt<RemoveItemResponse>();
                return Results.Ok(response);
            });
        }
    }
}
