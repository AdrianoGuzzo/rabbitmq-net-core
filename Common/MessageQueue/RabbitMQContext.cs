using Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace MessageQueue
{
    public class RabbitMQContext : IRabbitMQContext, IDisposable
    {
        private readonly ConnectionFactory factory;
        private readonly IConnection connection;
        private readonly IModel channel;

        public string ServiceId { get; set; }

        public RabbitMQContext(string serviceId, string userName, string password, string hostName)
        {
            ServiceId = serviceId;
            factory = new ConnectionFactory() { UserName = userName, Password = password, HostName = hostName };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }
        public void Send<T>(string queueName, Header<T> sendObj) where T : class
        {
            sendObj.ServiceId = ServiceId;
            var jsonString = JsonSerializer.Serialize(sendObj);
            channel.QueueDeclare(queue: queueName,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish(exchange: "",
                                routingKey: queueName,
                                basicProperties: null,
                                body: body);
            PrintModelInConsole(sendObj);
        }
        public void Receive<T>(string queueName) where T : class
        {
            channel.QueueDeclare(queue: queueName,
                                   durable: false,
                                   exclusive: false,
                                   autoDelete: false,
                                   arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var obj = JsonSerializer.Deserialize<Header<T>>(message);
                PrintModelInConsole(obj);
            };
            channel.BasicConsume(queue: queueName,
                                autoAck: true,
                                consumer: consumer);
            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();

        }

        private void PrintModelInConsole<T>(Header<T> sendObj) where T : class
        {

            Console.WriteLine("ServiceId: {0}", sendObj.ServiceId);
            Console.WriteLine("Id: {0}", sendObj.Id);
            Console.WriteLine("Timestamp: {0}", sendObj.Timestamp);
            foreach (PropertyInfo pinfo in sendObj.Response.GetType().GetProperties())
                Console.WriteLine("{0}: {1}", pinfo.Name, pinfo.GetValue(sendObj.Response, null).ToString());
            Console.WriteLine("");

        }

        public void Dispose()
        {
            if (!channel.IsClosed)
                channel.Close();
            if (connection.IsOpen)
                connection.Close();
        }

    }
}
