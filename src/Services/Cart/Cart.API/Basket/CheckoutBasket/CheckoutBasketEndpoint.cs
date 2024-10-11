using Cart.API.DTOs;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cart.API.Basket.CheckoutBasket
{
    public record CheckoutBasketRequest(BasketCheckoutDTO BasketCheckoutDto);
    public record CheckoutBasketResponse(bool IsSuccess);
    public class CheckoutBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/cart/checkout",[Authorize]async(CheckoutBasketRequest request,ISender sender, ClaimsPrincipal user) =>
            {
                string userId = user.FindFirst(ClaimTypes.NameIdentifier!)!.Value;
                request.BasketCheckoutDto.CustomerId =new Guid(userId);
                var command = request.Adapt<CheckoutBasketCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CheckoutBasketResponse>();
                return response;
            });
        }
    }
}
