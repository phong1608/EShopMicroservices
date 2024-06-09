using Carter;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Catalog.API.Products.CreateProduct;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryResponse(IEnumerable<Product> Product);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{CategoryName}", async (ISender sender,string CategoryName) =>
            {
                var result =await sender.Send(new GetProductByCategoryQuery(CategoryName));
                var response = result.Adapt<GetProductByCategoryResponse>();
                
                return response;
            })
            .WithName("GetProductByCategory")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Category")
            .WithDescription("Get Product By Category");
        }
    }
}
