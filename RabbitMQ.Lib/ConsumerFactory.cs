using System;
using RabbitMQ.Client;

namespace RabbitMQ.Lib
{
    /// <summary>
    /// Used to create RabbitMQ Consumers of a connection
    /// </summary>
    public class Connector
    {
        private ConnectionFactory _connectionFactory;

        /// <summary>
        /// Create a consumer factory instance, requires the full connection
        /// string to the RabbitMQ instance
        /// </summary>
        /// <param name="connectionString">connection string with username, password, and host</param>
        public Connector(string connectionString)
        {
            _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
        }

        public Consumer CreateConsumer()
        {
            return new Consumer(_connectionFactory);
        }

        public Producer CreateProducer()
        {
            return new Producer(_connectionFactory);
        }
    }
}
