using BuildingBlocks.Messaging.Events;
using Mapster;
using Marten;
using MassTransit;
using MediatR;
using Notification.API.Model;
using Notification.API.Notification.Command.CreateNotification;

namespace Notification.API.Notification.Events
{
    public class OrderCreatedEventHandler : IConsumer<OrderEvent>
    {
        private readonly ISender _sender;
        public OrderCreatedEventHandler(ISender sender)
        {
            _sender = sender;
        }
        public async Task Consume(ConsumeContext<OrderEvent> context)
        {
            var newNotification = new CreateNotificationCommand("Order Placed", context.Message.OrderId.ToString(), context.Message.CustomerId);
            await _sender.Send(newNotification);

        }
    }
}
