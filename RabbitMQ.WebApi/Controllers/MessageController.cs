using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.WebApi.Services;

namespace RabbitMQ.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private MessageProducer _producer;

        public MessageController(MessageProducer producer)
        {
            _producer = producer;
        }
        
        [HttpGet]
        public string Get()
        {
            return "hi omw you found me";
        }

        [HttpPost]
        public string Post(Message message)
        {
            _producer.SendMessage(message.Text);
            return message.Text;
        }
    }

    public class Message 
    {
      public string Text { get; set; }
    }
}
