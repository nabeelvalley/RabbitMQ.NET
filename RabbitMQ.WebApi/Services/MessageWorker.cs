using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Lib;

namespace RabbitMQ.WebApi.Services
{
    public class MessageWorker : BackgroundService
    {
        private Consumer _consumer;
        private readonly string _connectionString;
        private readonly string _queue;
        private readonly Action<string> _onMessageReceived;

        public MessageWorker(string connectionString, string queue, Action<string> onMessageReceived)
        {
            _connectionString = connectionString;
            _queue = queue;
            _onMessageReceived = onMessageReceived;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var connector = new Connector(_connectionString);
            _consumer = connector.CreateConsumer();

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _consumer.SimpleConsume(_queue, _onMessageReceived);

            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Dispose();
            await base.StopAsync(cancellationToken);
        }
    }
}
