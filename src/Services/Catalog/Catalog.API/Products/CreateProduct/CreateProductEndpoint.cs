

using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile,int Stock, decimal? Price);
    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        
        void ICarterModule.AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", [Authorize(Roles = "Admin")] async  (CreateProductRequest request, ISender sender) =>
            {
                //DTO
                var command = request.Adapt<CreateProducCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateProductResponse>();
                return Results.Created($"products/{response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("CreateProduct")
            .WithDescription("CreateProduct")
            ;
        }
    }
}
