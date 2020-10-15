using System;
using System.Text;
using MessageQueue;
using MessageQueue.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Receive
{
    class Program
    {
        private const string SERVICE_ID = "receive_service";
        private const string USER_NAME_RABBITMQ = "adriano";
        private const string PASSWORD_RABBITMQ = "1234";
        private const string HOSTNAME_RABBITMQ = "127.0.0.1";

        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddScoped<IRabbitMQContext, RabbitMQContext>(x => new RabbitMQContext(SERVICE_ID, USER_NAME_RABBITMQ, PASSWORD_RABBITMQ, HOSTNAME_RABBITMQ))
            .AddScoped<IQueueService, QueueService>()
            .BuildServiceProvider();

            serviceProvider
            .GetService<ILoggerFactory>();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogDebug("Starting application");
            IQueueService queueService = serviceProvider.GetService<IQueueService>();

            queueService.StartReceive();

           

        }
    }
}
