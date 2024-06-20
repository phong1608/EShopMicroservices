using Ordering.Application.Exceptions;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        private readonly IApplicationDbContext _context;
        public UpdateOrderHandler(IApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = command.Order.Id;
            var order = await _context.Orders.FindAsync(orderId,cancellationToken);
            if (order == null)
            {
                throw new OrderNotFoundException(orderId);
            }
            UpdateOrder(order, command.Order);
            await _context.SaveChangesAsync(cancellationToken);
            return new UpdateOrderResult(true);
        }
        public void UpdateOrder(Order order,OrderDTO orderDTO)
        {
            var shippingAddress = Address.Of(orderDTO.ShippingAddress.FirstName, orderDTO.ShippingAddress.LastName, orderDTO.ShippingAddress.Email,
               orderDTO.ShippingAddress.Phone, orderDTO.ShippingAddress.City, orderDTO.ShippingAddress.District, orderDTO.ShippingAddress.Street);
            var payment = Payment.Of(orderDTO.Payment.CardName, orderDTO.Payment.CardNumber, orderDTO.Payment.Expiration, orderDTO.Payment.Cvv, orderDTO.Payment.PaymentMethod);
            order.Update(
                orderId: orderDTO.Id,
                customerId: orderDTO.CustomerId,
                orderName:  orderDTO.OrderName,
                shippingAddress: shippingAddress,
                payment: payment,
                orderStatus: orderDTO.Status);
        }
    }
}
