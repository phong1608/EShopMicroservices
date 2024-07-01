using Catalog.gRPC.Models;
using Catalog.gRPC.Protos;
using Grpc.Core;
using Marten;
using System.Threading;

namespace Catalog.gRPC.Services
{
    public class ProductService : GetProductService.GetProductServiceBase
    {
        private readonly IDocumentSession _session;
        private readonly ILogger<ProductService> _logger;
        public ProductService(ILogger<ProductService> logger,IDocumentSession session)
        {
            _logger = logger;
            _session = session;
        }
        public override async Task<ProductModel> GetProuductInfo(GetProductRequest request, ServerCallContext context)
        {
            var productId = new Guid(request.Id);
            var product = await _session.Query<Product>().FirstOrDefaultAsync(c => c.Id ==productId);
            if (product == null)
            {
                
            }
            var productModel = new ProductModel() { ProductName=product!.Name, Price=(double)product.Price};
            return productModel;
        }
    }
}
