
using Ordering.Application.Data;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        private readonly IApplicationDbContext _context;
        public CreateOrderHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = CreateNewOrder(command.Order);
            var customer = await _context.Customers.FindAsync(command.Order.CustomerId);
            if (customer == null)
            {
                throw new Exception();
            }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return new CreateOrderResult(command.Order.Id);
        }
        public Order CreateNewOrder(OrderDTO order)
        {
            var shippingAddress = Address.Of(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.Email,
                order.ShippingAddress.Phone, order.ShippingAddress.City, order.ShippingAddress.District, order.ShippingAddress.Street);
            var newOrder = Order.Create(
                orderId: Guid.NewGuid(),
                customerId: order.CustomerId,
                orderName: order.OrderName,
                shippingAddress: shippingAddress,
                payment: Payment.Of(order.Payment.CardName, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.Cvv, order.Payment.PaymentMethod),
                orderStatus:OrderStatus.Draft
                );
            foreach(var itemDto in order.Items)
            {
                newOrder.Add(itemDto.ProductId, itemDto.Quantity, itemDto.Price);
            }
            return newOrder;
        }
    }
}
