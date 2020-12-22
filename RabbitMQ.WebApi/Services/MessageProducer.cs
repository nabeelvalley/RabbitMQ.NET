using System;
using RabbitMQ.Lib;

namespace RabbitMQ.WebApi.Services
{
    public class MessageProducer : IDisposable
    {
        private string _connectionString;
        private string _queue;
        private Producer _producer;

        public MessageProducer(string connectionString, string queue)
        {
            _connectionString = connectionString;
            _queue = queue;
            _producer = new Connector(connectionString).CreateProducer();
        }

        public void SendMessage(string message)
        {
            _producer.Publish(_queue, message);
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}
