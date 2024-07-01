using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(Guid Id) :ICommand<DeleteOrderResult>;
    public record DeleteOrderResult(bool IsSuccess);
    public class DeleteOrderCommandValidator :AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {

            
        }
    }
}
