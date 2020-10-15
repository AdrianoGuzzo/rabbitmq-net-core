using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageQueue
{
    public interface IRabbitMQContext : IDisposable
    {
        public string ServiceId { get; set; }
        void Send<T>(string QueueName, Header<T> sendObj) where T : class;
        void Receive<T>(string queueName) where T : class;
    }
}
