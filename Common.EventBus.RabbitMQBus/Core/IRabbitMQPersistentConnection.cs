using RabbitMQ.Client;
using System;

namespace Common.EventBus.RabbitMQBus.Core
{
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
    }
}
