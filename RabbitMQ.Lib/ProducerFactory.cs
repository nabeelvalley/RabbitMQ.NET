using System;
using RabbitMQ.Client;

namespace RabbitMQ.Lib
{
    /// <summary>
    /// Used to create RabbitMQ Producers
    /// </summary>
    public class ProducerFactory
    {
        private ConnectionFactory _connectionFactory;

        /// <summary>
        /// Create a producer factory instance, requires the full connection
        /// string to the RabbitMQ instance
        /// </summary>
        /// <param name="connectionString">connection string with username, password, and host</param>
        public ProducerFactory(string connectionString)
        {
            _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
        }

        public Consumer CreateProducer()
        {
            return new Consumer(_connectionFactory);
        }
    }
}
