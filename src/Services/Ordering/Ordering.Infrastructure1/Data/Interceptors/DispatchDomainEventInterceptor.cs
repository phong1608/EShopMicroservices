using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class DispatchDomainEventInterceptor : SaveChangesInterceptor
    {
        private readonly IMediator _mediator;
        public DispatchDomainEventInterceptor(IMediator mediator) 
        {
            _mediator = mediator;
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        public async Task DispatchDomainEvents(DbContext? context)
        {
            if (context == null) return;
            var aggregates = context.ChangeTracker.Entries<IAggregate>().Where(c => c.Entity.DomainEvents.Any()).Select(c => c.Entity);
            var domainEvents=aggregates.SelectMany(a=>a.DomainEvents).ToList();
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
                
            }
        }
    }
}
