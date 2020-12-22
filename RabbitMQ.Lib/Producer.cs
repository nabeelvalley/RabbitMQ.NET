using System;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Lib
{
    /// <summary>
    /// Simple RabbitMQ Producer, will remain active as long as the instance is
    /// kept alive
    /// </summary>
    public class Producer : Connection
    {
        internal Producer(ConnectionFactory connectionFactory) : base(connectionFactory)
        { }

        /// <summary>
        /// Publish message to a queue
        /// </summary>
        /// <param name="queue">queue to consume</param>
        /// <param name="handler">event handler to handle consume</param>
        public void Publish(string queue, string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            Channel.QueueDeclare(queue, true, false, false, null);
            Channel.BasicPublish("", queue, null, body);
        }
    }
}
