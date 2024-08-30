using Ordering.Domain.Abstractions;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Events
{
        public record UpdateOrderEvent(Guid orderId,Guid customerId,OrderStatus Status) : IDomainEvent;

}
