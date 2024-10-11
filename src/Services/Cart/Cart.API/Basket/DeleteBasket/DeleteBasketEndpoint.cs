using Cart.API.Basket.GetBasket;
using Mapster;

namespace Cart.API.Basket.DeleteBasket
{
    //public record DeleteBasketRequest(string UserName);
    public record DeleteBasketResponse(bool IsSuccess);
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/cart/{userId}", async(ISender sender,Guid userId) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(userId));
                var response = result.Adapt<DeleteBasketResponse>();
                return Results.Ok(response);
            })
            .WithName("DeleteBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("DeleteBasket")
            .WithDescription("DeleteBasket");
        }
    }
}
