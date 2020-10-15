using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace MessageQueue.Service
{
    public class QueueService : IQueueService, IDisposable
    {

        private readonly IRabbitMQContext rabbitMQContext;
        private const string QUEUE_NAME = "FilaExemplo";
        public QueueService(IRabbitMQContext rabbitMQContext)
        {
            this.rabbitMQContext = rabbitMQContext;
        }
        public void StartJobSend()
        {
            Timer aTimer = new System.Timers.Timer(5000);

            aTimer.Elapsed += SendMessage;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public void StartReceive()
        {
            rabbitMQContext.Receive<Message>(QUEUE_NAME);
        }

        private void SendMessage(Object source, ElapsedEventArgs e)
        {
            rabbitMQContext.Send<Message>(QUEUE_NAME, new Header<Message>()
            {
                Id = Guid.NewGuid().ToString(),
                Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
                Response = new Message("Hello World")
            });
        }
        public void Dispose()
        => rabbitMQContext.Dispose();
    }
}
