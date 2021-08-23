using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JobConsumer
{
    public class QueueManager
    {
        private IModel _channel;
        public QueueManager()
        {
            //CreateQueue();     
        }

        public void CreateQueue(byte[] Message)
        {
            ConnectionFactory factory = new ConnectionFactory();

            //factory.Uri = new Uri("amqps://pdkxismq:K0UjQRlq3pXaPZf2rTDhK-UrghiFPTRK@elk.rmq2.cloudamqp.com/pdkxismq");
            factory.Uri = new Uri("amqp://localhost:5672/");
            factory.UserName = "guest";
            factory.Password = "guest";

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare("dataqueue", false, false, false);
            _channel = channel;

            channel.BasicPublish("", "dataqueue", body: Message);
            channel.Dispose();
        }

        public void SendMessagetoQueue(byte[] Message)
        {
        }
    }
}
