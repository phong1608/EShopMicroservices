using BuildingBlocks.Messaging.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Commands.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandlers.Intergration
{
    public class BasketCheckoutEventHandler : IConsumer<BasketCheckoutEvent>
    {
        private readonly ILogger<BasketCheckoutEventHandler> _logger;
        private readonly ISender _sender;

        public BasketCheckoutEventHandler(ILogger<BasketCheckoutEventHandler> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            _logger.LogInformation("IntegrationEvent handled: {IntegrationEvent}", context.Message.GetType().Name);
            var command = MapToCreateOrderCommand(context.Message);
            await _sender.Send(command);
        }
        private static CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
        {
            // Create full order with incoming event data
            var addressDto = new AddressDTO(message.FirstName, message.LastName, message.EmailAddress, message.PhoneNumber, message.City, message.District, message.Street);
            var orderId = Guid.NewGuid();
            
            var itemList = message.CartItems.Select(x => new OrderItemDTO(orderId, x.ProductId, x.Quantity, x.Price)).ToList();
            var orderDto = new OrderDTO(
                id: orderId,
                customerId: message.CustomerId,
                orderName: message.UserName,
                shippingAddress: addressDto,
                status: Ordering.Domain.Enums.OrderStatus.Pending,
                orderItems: itemList)
                ;


            return new CreateOrderCommand(orderDto);
        }
    }
}
