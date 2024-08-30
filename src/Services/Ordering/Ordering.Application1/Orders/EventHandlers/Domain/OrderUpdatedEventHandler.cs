using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    internal class OrderUpdatedEventHandler : INotificationHandler<UpdateOrderEvent>
    {
        private readonly ILogger<OrderUpdatedEventHandler> _logger;
        private readonly IPublisher _publisher;
        public OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger, IPublisher publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }
        public Task Handle(UpdateOrderEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
