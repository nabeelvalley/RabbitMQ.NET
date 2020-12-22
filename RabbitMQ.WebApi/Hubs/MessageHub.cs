using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.WebApi.Services;

namespace RabbitMQ.WebApi.Hubs
{
    public interface IMessageHubClient
    {
        Task OnMessageReceived(string message);
    }

    public class MessageHub : Hub<IMessageHubClient>
    {
        private MessageProducer _producer;

        public MessageHub(MessageProducer producer)
        {
            _producer = producer;
        }

        public async Task SendMessage(string message)
        {
            _producer.SendMessage(message);
        }

        public override async Task OnConnectedAsync()
        {
            _producer.SendMessage("New Client Connected");
            await base.OnConnectedAsync();
        }

        

        private async Task BroadcastMessage(string message)
        {
            await Clients.All.OnMessageReceived(message);
        }
    }
}
