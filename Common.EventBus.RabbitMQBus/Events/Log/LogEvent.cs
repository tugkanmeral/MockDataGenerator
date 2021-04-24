using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventBus.RabbitMQBus.Events.Log
{
    public class LogEvent : EventBase
    {
        public string Message { get; set; }
    }
}
