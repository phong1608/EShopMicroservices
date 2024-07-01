using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Messaging.Events
{
    public class IntergrationEvent
    {
        public Guid Id=>Guid.NewGuid();
        public DateTime OccurredOn=>DateTime.Now;
        public string EvenType=>GetType().AssemblyQualifiedName!;
    }
}
