﻿using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductResult(bool IsDeleted);
    public record DeleteProductCommand(Guid Id):ICommand<DeleteProductResult>;
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        private readonly IDocumentSession _session;
        public DeleteProductCommandHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException(command.Id);
            }
            _session.Delete(product);
            await _session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
    }
}
