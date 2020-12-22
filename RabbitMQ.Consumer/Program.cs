using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Lib;

namespace RabbitMQ.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Consumer");

            var connectionString = "amqp://guest:guest@localhost:5672";
            var queue = "dotnet.messages.received";

            var consumerFactory = new Connector(connectionString);

            using (var consumer = consumerFactory.CreateConsumer())
            {
                consumer.SimpleConsume(queue, (message) =>
                {
                    Console.WriteLine(message);
                });

                Console.WriteLine("Press enter to terminate connections");
                Console.ReadLine();
            }
        }
    }
}
