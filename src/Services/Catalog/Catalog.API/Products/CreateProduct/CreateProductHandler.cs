
using BuildingBlocks.CQRS;
using Catalog.API.Models;
using FluentValidation;
using JasperFx.CodeGeneration.Frames;
using Marten;
using MediatR;
using System.Security.Claims;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProducCommand(string Name, List<string> Category, string Description, string ImageFile, decimal? Price):ICommand<CreateProductResult>;
    
    public record CreateProductResult(Guid Id);
    public class CreateProductCommandValidator : AbstractValidator<CreateProducCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must greater than 0");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");


        }

    }
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
