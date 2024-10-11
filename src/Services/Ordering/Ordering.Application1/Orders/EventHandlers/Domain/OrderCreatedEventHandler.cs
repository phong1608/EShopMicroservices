using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Application.OrdersExtension;
using Ordering.Domain.Abstractions;
using Ordering.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
    {
        private readonly IPublishEndpoint _pushlishEndpoint;
        private readonly IFeatureManager _featuerManager;
        private ILogger<OrderCreatedEventHandler> _logger;
        public OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger,IPublishEndpoint publishEndpoint,IFeatureManager featureManager)
        {
            _logger = logger;
            _pushlishEndpoint = publishEndpoint;
            _featuerManager = featureManager;
        }
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
            if(await _featuerManager.IsEnabledAsync("OrderFullfilment"))
            {
                 var orderCreatedIntegraionEvent = domainEvent.order.ToOrderDTO();
                 await _pushlishEndpoint.Publish(orderCreatedIntegraionEvent,cancellationToken);
            }

        }
    }
}
