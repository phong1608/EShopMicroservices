using Carter;
using Catalog.API.Models;
using Catalog.API.Products.CreateProduct;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Collections;

namespace Catalog.API.Products.GetProduct
{
    public record GetProductResponse(IEnumerable<Product> Products);
    public record GetProductRequest(int? PageNumber=1,int? PageSize=10);
    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async([AsParameters] GetProductRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetProductQuery>();
                var result = await sender.Send(query); 
                var response = result.Adapt<GetProductResponse>();
                
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products")
            
            ;
        }
    }
}
