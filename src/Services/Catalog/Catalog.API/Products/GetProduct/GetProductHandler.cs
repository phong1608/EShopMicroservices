using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProduct
{
    public record GetProductResult(IEnumerable<Product> Products);
    public record GetProductQuery:IQuery<GetProductResult>;
    public class GetProductQueryHandler : IQueryHandler<GetProductQuery, GetProductResult>

    {
        private readonly IDocumentSession _session;
        private readonly ILogger<GetProductQueryHandler> _logger;
        public GetProductQueryHandler(IDocumentSession session, ILogger<GetProductQueryHandler> logger)
        {
            _session = session;
            _logger = logger;
        }
        public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetProductQueryHandler.Hanlde called with {@Query}", query);
            var product = await _session.Query<Product>().ToListAsync(cancellationToken);
            return new GetProductResult(product);
        }
    }
}
