﻿using Cart.API.DTOs;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace Cart.API.Basket.GetBasket
{
    public record GetBasketResponse(CartResponseDTO CartDTO);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {  
            app.MapGet("/cart",[Authorize] async (ISender sender, ClaimsPrincipal user) =>
            {
                string userId = user.FindFirst(ClaimTypes.NameIdentifier!)!.Value;
                var result = await sender.Send(new GetBasketQuery(new Guid(userId)));
                var repsonse = result.Adapt<GetBasketResponse>();
                return Results.Ok(result);
            })
            .WithName("GetBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("GetBasket")
            .WithDescription("GetBasket");
        }
    }
}
