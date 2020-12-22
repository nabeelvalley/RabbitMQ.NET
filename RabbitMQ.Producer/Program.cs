using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Lib;

namespace RabbitMQ.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Producer");

            var connectionString = "amqp://guest:guest@localhost:5672";
            var queue = "dotnet.messages.received";

            var consumerFactory = new Connector(connectionString);

            using (var producer = consumerFactory.CreateProducer())
            {
                producer.Publish(queue, "Hello matey");

                Console.WriteLine("Press enter to terminate connections");
                Console.ReadLine();
            }
        }
    }
}
