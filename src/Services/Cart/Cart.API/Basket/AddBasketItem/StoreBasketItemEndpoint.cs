﻿using Cart.API.DTOs;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(CartItemsDTO Item,string UserId);
public record StoreBasketResponse(bool ISuccess);

public class StoreBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket",[Authorize] async (StoreBasketRequest request, ISender sender, ClaimsPrincipal user) =>
        {

            string userId = user.FindFirst(ClaimTypes.NameIdentifier!)!.Value;
            
            var command = new StoreBasketCommand(request.Item, userId) ;

            var result = await sender.Send(command);

            var response = result.Adapt<StoreBasketResponse>();

            return response;
        })
        .WithName("CreateProduct")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}