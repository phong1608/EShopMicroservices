using BuildingBlocks;
using BuildingBlocks.Messaging.Events;
using Catalog.gRPC.Protos;
using Ordering.Application.Orders.Queries.GetOrder;
using Ordering.Application.OrdersExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        private readonly IApplicationDbContext _context;
        public GetOrdersHandler(IApplicationDbContext context )
        {
            _context = context;
        }
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex =query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;
            var totalCount = await _context.Orders.LongCountAsync(cancellationToken);
            var orders =await _context.Orders
                .Include(x=>x.OrderItems)
                .Skip(pageSize*pageIndex)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
            var orderDTO = orders.ToOrderDtoList();
            return new GetOrdersResult(new PaginatedResult<OrderDTO>(pageIndex, pageSize, totalCount, orderDTO));
        }
        
    }
}
