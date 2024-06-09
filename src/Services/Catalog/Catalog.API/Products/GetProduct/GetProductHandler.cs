using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;
using Marten.Pagination;

namespace Catalog.API.Products.GetProduct
{
    public record GetProductResult(IEnumerable<Product> Products);
    public record GetProductQuery(int? PageNumber = 1, int? PageSize = 10) :IQuery<GetProductResult>;
    public class GetProductQueryHandler : IQueryHandler<GetProductQuery, GetProductResult>

    {
        private readonly IDocumentSession _session;
        public GetProductQueryHandler(IDocumentSession session)
        {
            _session = session;
            
        }
        public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            var product = await _session.Query<Product>()
                                        .ToPagedListAsync(query.PageNumber ?? 1,query.PageSize ?? 10,cancellationToken);
            return new GetProductResult(product);
        }
    }
}
