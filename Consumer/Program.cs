using JobConsumer;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            HubConnection connectionSignalR = new HubConnectionBuilder().WithUrl("https://localhost:5003/jobhub").Build();
            connectionSignalR.StartAsync();

            ConnectionFactory factory = new ConnectionFactory();
            //factory.Uri = new Uri("amqps://pdkxismq:K0UjQRlq3pXaPZf2rTDhK-UrghiFPTRK@elk.rmq2.cloudamqp.com/pdkxismq");
            factory.Uri = new Uri("amqp://localhost:5672/");
            factory.UserName = "guest";
            factory.Password = "guest";

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare("dataqueue", false, false, false);
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("dataqueue", true, consumer);
            consumer.Received += (s, e) =>
           {

               try
               {
                 
                       var serializeData2 = Encoding.UTF32.GetString(e.Body.Span);                      

                       QueueModel data = new QueueModel();
                       data = JsonConvert.DeserializeObject<QueueModel>(serializeData2);                      

                       Console.WriteLine($"{data.Frekans}");
                       Console.WriteLine($"{data.JobModel.Kod}");
                       Console.WriteLine($"{data.JobModel.Okuyucu}");
                       Console.WriteLine($"{data.JobModel.Uzaklik}");
                       Console.WriteLine($"{data.JobModel.VeriNo}");
                       Console.WriteLine($"{data.JobModel.Tarih}");
                       connectionSignalR.InvokeAsync("SendMessageAsync", JsonConvert.SerializeObject(data));
                   

               }
               catch (Exception ex)
               {

                   throw;
               }
           };
            Console.Read();



        }

    }
}
