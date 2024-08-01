using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer
{
    public record GetOrderByCustomerQuery(Guid CustomerId) :IQuery<GetOrderByCustormerResult>;
    public record GetOrderByCustormerResult(IEnumerable<OrderDTO> Orders);
}
