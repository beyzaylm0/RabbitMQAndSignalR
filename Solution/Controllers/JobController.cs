using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using Solution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Solution.Controllers

{

    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IModel _channel;
        [HttpPost()]
       
        public IActionResult Post(byte[] Message)
        {

            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://localhost:5672/");
            factory.UserName = "guest";
            factory.Password = "guest";

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare("dataqueue", false, false, false);
            _channel = channel;

            channel.BasicPublish("", "dataqueue", body: Message);
            channel.Dispose();
            return Ok();
        }


    }
}

