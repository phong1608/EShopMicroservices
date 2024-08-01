using BuildingBlocks.CQRS;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDTO Order):ICommand<CreateOrderResult>;
    public record CreateOrderResult(Guid Id);
    public class CreateOrderCommandValidator: AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("CustomerId is required");
            RuleFor(x => x.Order.Items).NotEmpty().WithMessage("OrderItems is required");

        }
    }
    
}
