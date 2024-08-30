using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Messaging.Events
{
    public class NotificationEvent: IntergrationEvent
    {
        public Guid RecipientId { get; set; }
        public DateTime SendDate { get; set; } = DateTime.UtcNow;
        public bool IsSeen = false;
        public string Content { get; set; } = default!;

    }
}
