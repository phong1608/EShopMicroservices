using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using FluentValidation;
using Marten;
using MediatR;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal? Price):ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Product Id is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must greater than 0");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }
    }

    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        private readonly IDocumentSession _session;
        public UpdateProductCommandHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _session.LoadAsync<Product>(command.Id,cancellationToken);
            if(product is null)
            {
                throw new ProductNotFoundException(command.Id);
            }
            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;
            _session.Update(product);
            await _session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}
