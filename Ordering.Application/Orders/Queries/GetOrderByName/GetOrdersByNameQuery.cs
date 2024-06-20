using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public record GetOrdersByNameQuery(string Name) : IQuery<GetOrdersByNameResult>;
    public record GetOrdersByNameResult(IEnumerable<OrderDTO> Orders);
    public class GetOrdersByNameValidator:AbstractValidator<GetOrdersByNameQuery>
    {
        public GetOrdersByNameValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("OrderName is required");
        }
    }
}
