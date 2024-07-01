using Carter;
using Catalog.API.Products.CreateProduct;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Catalog.API.Products.DeleteProduct
{
    //public record DeleteProductRequest(Guid Id);
    public record DeleteProductResponse(bool IsDeleted);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{Id}", [Authorize(Roles = "Admin")] async (ISender sender,Guid Id) =>
            {
                var result = await sender.Send(new DeleteProductCommand(Id));
                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);

            })
            .WithName("DeleteProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product ")
            .WithDescription("Delete Product");
        }
    }
}
