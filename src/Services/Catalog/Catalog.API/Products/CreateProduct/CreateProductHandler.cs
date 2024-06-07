
using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;
using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProducCommand(string Name, List<string> Category, string Description, string ImageFile, decimal? Price):ICommand<CreateProductResult>;
    
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler : ICommandHandler<CreateProducCommand, CreateProductResult>
    {
        private readonly IDocumentSession _session;
        public CreateProductCommandHandler(IDocumentSession session)
        {
            _session = session;
        }
        public async Task<CreateProductResult> Handle(CreateProducCommand command, CancellationToken cancellationToken)
        {
            var product = new Product 
            {
                Name=command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
                
            };
            _session.Store(product);
            await _session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }   

    }
}
