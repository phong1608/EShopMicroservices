using Cart.API.DTOs;
using Mapster;
namespace Cart.API.Basket.GetBasket
{
    public record GetBasketResponse(CartResponseDTO CartDTO);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {  
            app.MapGet("/basket/{userId}", async (Guid userId, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userId));
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
