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
        private readonly ILogger<GetProductByCategoryQueryHandler> _logger;
        public GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
        {
            _session = session;
            _logger = logger;
        }

        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var product = await _session.Query<Product>()
                                        .Where(p => p.Category.Contains(query.CategoryName))
                                        .ToListAsync();
            return new GetProductByCategoryResult(product);
        }
    }
}
