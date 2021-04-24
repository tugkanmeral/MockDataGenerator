using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Services.Logger.MongoLogger.EventBusConsumers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Logger.MongoLogger.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static LogQueueConsumer Consumer { get; set; }

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            Consumer = app.ApplicationServices.GetService<LogQueueConsumer>();
            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);
            life.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            Consumer.Consume();
        }

        private static void OnStopping()
        {
            Consumer.Disconnect();
        }
    }
}
