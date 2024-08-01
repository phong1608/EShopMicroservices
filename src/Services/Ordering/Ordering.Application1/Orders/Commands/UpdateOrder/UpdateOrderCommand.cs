using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDTO Order) :ICommand<UpdateOrderResult>;
    public record UpdateOrderResult(bool IsSuccess);
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x=>x.Order.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Order Name is required");
            RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("CustomerId is required");
        }
    }
    
}
