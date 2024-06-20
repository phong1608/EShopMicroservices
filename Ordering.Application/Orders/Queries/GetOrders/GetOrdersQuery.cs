using BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrder
{
    public record GetOrdersQuery(PaginationRequest PaginationRequest):IQuery<GetOrdersResult>;
    public record GetOrdersResult(PaginatedResult<OrderDTO> Orders);
}
