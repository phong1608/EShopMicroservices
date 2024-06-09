using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) :IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        private readonly IDocumentSession _session;
        public GetProductByIdQueryHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _session.Query<Product>().FirstAsync(c => c.Id == request.Id,cancellationToken);
            if(product is null)
            {
                throw new ProductNotFoundException(request.Id);
            }
            return new GetProductByIdResult(product);
        }
    }
}
