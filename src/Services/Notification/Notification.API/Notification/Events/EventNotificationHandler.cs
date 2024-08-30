using BuildingBlocks.Messaging.Events;
using Mapster;
using Marten;
using MassTransit;
using MediatR;
using Notification.API.Model;
using Notification.API.Notification.Command.CreateNotification;

namespace Notification.API.Notification.Events
{
    public class EventNotificationHandler : IConsumer<NotificationEvent>
    {
        private readonly IDocumentSession _session;
        private readonly ISender _sender;
        public EventNotificationHandler(IDocumentSession session,ISender sender)
        {
            _session = session;
            _sender = sender;
        }
        public async Task Consume(ConsumeContext<NotificationEvent> context)
        {
            var newNotification = context.Message.Adapt<CreateNotificationCommand>();
            await _sender.Send(newNotification);



        }
    }
}
