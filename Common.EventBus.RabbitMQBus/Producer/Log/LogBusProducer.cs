using Common.EventBus.RabbitMQBus.Core;
using Common.EventBus.RabbitMQBus.Events;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.EventBus.RabbitMQBus.Producer.Log
{
    public class LogBusProducer
    {
        private readonly IRabbitMQPersistentConnection persistentConnection;
        private readonly ILogger<LogBusProducer> logger;
        private readonly int retryCount;

        public LogBusProducer(IRabbitMQPersistentConnection rabbitMQPersistentConnection, ILogger<LogBusProducer> logger, int retryCount = 5)
        {
            this.persistentConnection = rabbitMQPersistentConnection;
            this.logger = logger;
            this.retryCount = retryCount;
        }

        public void Publish(string queueName, EventBase @event)
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();

            var policy = RetryPolicy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (ex, time) =>
                    {
                        logger.LogWarning(ex, "RabbitMQ Client could not connect after {TimeOut}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                    }
                );

            using var channel = persistentConnection.CreateModel();
            /*
            durable =>      if setted false then queue will be held in in-memory. 
                            if setted true then queue will be held in physical memory and do not be gone when host is restarted
            exclusive =>    if setted true then just one client subsvribe to the queue. Queue will be deleted the subscriber unsubscribe the queue.
                            if setted false then work as default
            autoDelete =>   if setted true then the queue that has had at least one consumer is deleted when last consumer unsubscribes
            arguments =>    used by plugins and broker-specific features such as message TTL, queue length limit etc.
             */
            channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            policy.Execute(() =>
            {
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;

                channel.ConfirmSelect();
                channel.BasicPublish(
                    exchange: "",
                    routingKey: queueName,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
                channel.WaitForConfirmsOrDie();

                channel.BasicAcks += (sender, eventArgs) =>
                {
                    Console.WriteLine("Sent RabbitMQ");
                };
            });
        }
    }
}
