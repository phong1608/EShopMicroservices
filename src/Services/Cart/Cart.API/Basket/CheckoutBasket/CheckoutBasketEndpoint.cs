﻿using Cart.API.DTOs;
using Mapster;

namespace Cart.API.Basket.CheckoutBasket
{
    public record CheckoutBasketRequest(BasketCheckoutDTO BasketCheckoutDto);
    public record CheckoutBasketResponse(bool IsSuccess);
    public class CheckoutBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/checkout",async(CheckoutBasketRequest request,ISender sender) =>
            {
                var command = request.Adapt<CheckoutBasketCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CheckoutBasketResponse>();
                return response;
            });
        }
    }
}
