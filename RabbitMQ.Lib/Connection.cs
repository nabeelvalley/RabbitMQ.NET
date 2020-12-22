using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Lib
{
    /// <summary>
    /// Simple RabbitMQ Connection, will remain activeprovided the consumer 
    /// instance is kept alive
    /// </summary>
    public class Connection : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        public IModel Channel { get; }

        internal Connection(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _connection = _connectionFactory.CreateConnection();

            Channel = _connection.CreateModel();
        }

        /// <summary>
        /// Dispose the channel and connection instances for a RabbitMQ
        /// connection
        /// </summary>
        public void Dispose()
        {
            _connection.Dispose();
            Channel.Dispose();
        }
    }
}
