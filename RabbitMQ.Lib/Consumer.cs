using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Lib
{
    /// <summary>
    /// Simple RabbitMQ Consumer, will continue to consume provided the consumer
    /// instance is kept alive
    /// </summary>
    public class Consumer : Connection
    {
        internal Consumer(ConnectionFactory connectionFactory) : base(connectionFactory)
        { }

        /// <summary>
        /// Consume a queue using the the provided handler which will receive
        /// the message body as a string as well as auto-acks if handler does
        /// not throw. Can use Sync and Async handlers
        /// </summary>
        /// <param name="queue">queue to consume</param>
        /// <param name="handler">event handler to handle consume</param>
        public void SimpleConsume(string queue, Action<string> handler)
        {
            RawConsume(queue, (sender, ev) =>
            {
                var body = ev.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);

                handler(message);

                Channel.BasicAck(ev.DeliveryTag, false);
            });
        }

        /// <summary>
        /// Consume a queue using a handler that's provided directly to the
        /// RabbitMQ.Client lib's EventHandler
        /// </summary>
        /// <param name="queue">queue to consume</param>
        /// <param name="handler">event handler</param>
        public void RawConsume(string queue, Action<object, BasicDeliverEventArgs> handler)
        {
            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (sender, ev) => handler(sender, ev); ;

            Channel.BasicConsume(queue, false, consumer);
        }
    }
}
