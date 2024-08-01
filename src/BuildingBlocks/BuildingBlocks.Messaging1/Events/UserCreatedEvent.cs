using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Messaging.Events
{
    public class UserCreatedEvent :IntergrationEvent
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }   
    }
}
