using BuildingBlocks;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Queries.GetOrder;

namespace Ordering.API.Endpoints
{
    public record GetOrdersResponse(PaginatedResult<OrderDTO> Orders);
    public class GetOrder :ICarterModule
    {
      

            public void AddRoutes(IEndpointRouteBuilder app)
            {
                app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
                {
                    var result = await sender.Send(new GetOrdersQuery(request));

                    var response = result.Adapt<GetOrdersResponse>();

                    return Results.Ok(response);
                })
                .WithName("GetOrders")
                .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Orders")
                .WithDescription("Get Orders");
            }
        
    }
}
