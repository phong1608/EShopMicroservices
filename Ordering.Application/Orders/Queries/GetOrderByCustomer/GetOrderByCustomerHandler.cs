using Ordering.Application.OrdersExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer
{
    public class GetOrderByCustomerHandler : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustormerResult>
    {
        private readonly IApplicationDbContext _context;
        public GetOrderByCustomerHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GetOrderByCustormerResult> Handle(GetOrderByCustomerQuery query, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .Include(x => x.OrderItems)
                .AsNoTracking()
                .Where(x => x.CustomerId == query.CustomerId)
                .ToListAsync(cancellationToken);
            return new GetOrderByCustormerResult(orders.ToOrderDtoList());
        }
    }
}
