using Common.EventBus.RabbitMQBus.Core;
using Common.EventBus.RabbitMQBus.Events.Log;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.Logger.MongoLogger.Entities;
using Services.Logger.MongoLogger.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Logger.MongoLogger.EventBusConsumers
{
    public class LogQueueConsumer
    {
        private readonly IRabbitMQPersistentConnection _connection;
        private ILogRepository _logRepository;

        public LogQueueConsumer(IRabbitMQPersistentConnection connection, ILogRepository logRepository) //, LogRepository logRepository
        {
            _connection = connection;
            _logRepository = logRepository;
        }

        public void Consume()
        {
            if (!_connection.IsConnected)
            {
                _connection.TryConnect();
            }

            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: QueueNames.LOG_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: QueueNames.LOG_QUEUE, autoAck: true, consumer: consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.Span);
            var @event = JsonConvert.DeserializeObject<LogEvent>(message);

            if (e.RoutingKey == QueueNames.LOG_QUEUE)
            {
                Log log = new();
                log.Message = @event.Message;
                _logRepository.InsertOneAsync(log);   
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}
