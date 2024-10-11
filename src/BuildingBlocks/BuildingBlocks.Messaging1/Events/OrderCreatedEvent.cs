using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Messaging.Events
{
    public class OrderEvent
    {
        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }
        public string OrderName { get;  set; } = default!;
        public string Status { get; set; }=default!;
    }
}
