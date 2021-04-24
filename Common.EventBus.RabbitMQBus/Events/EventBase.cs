using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventBus.RabbitMQBus.Events
{
    public class EventBase
    {
        public Guid RequestId { get; private init; }
        public DateTime CreationTime { get; private init; }

        public EventBase()
        {
            RequestId = Guid.NewGuid();
            CreationTime = DateTime.UtcNow;
        }
    }
}
