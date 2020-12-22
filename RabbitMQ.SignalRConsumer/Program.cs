using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace RabbitMQ.SignalRConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("SignalR Consumer");

            var connection = new HubConnectionBuilder()
                                 .WithUrl("http://localhost:5000/messagehub")
                                 .WithAutomaticReconnect()
                                 .Build();

            // ensure you register listeners before starting the connections
            connection.On<string>("OnMessageReceived", (message) =>
            {
                Console.WriteLine("MessageReceived: " + message);
            });
          
            // start a connection
            await connection.StartAsync();

            // send message with connection
            await connection.InvokeAsync<string>("SendMessage", "Test Message from SignalR Consumer");

            Console.WriteLine("Press enter to close");
            Console.ReadLine();
        }
    }
}
