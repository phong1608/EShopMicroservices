using Ordering.Application.OrdersExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public class GetOrdersByNameHanlder : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        private readonly IApplicationDbContext _context;
        public GetOrdersByNameHanlder(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(x=>x.OrderItems)
                .Where(x=>x.OrderName==query.Name)
                .ToListAsync(cancellationToken);

            return new GetOrdersByNameResult(order.ToOrderDtoList());
        }
         
    }
}
