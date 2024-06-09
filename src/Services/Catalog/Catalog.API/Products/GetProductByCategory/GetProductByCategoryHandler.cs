using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryResult(IEnumerable<Product> Product);
    public record GetProductByCategoryQuery(string CategoryName):IQuery<GetProductByCategoryResult>;
    internal class GetProductByCategoryQueryHandler : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        private readonly IDocumentSession _session;
        public GetProductByCategoryQueryHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var product = await _session.Query<Product>()
                                        .Where(p => p.Category.Contains(query.CategoryName))
                                        .ToListAsync(cancellationToken);
            return new GetProductByCategoryResult(product);
        }
    }
}
