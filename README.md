# References: 

- [DotNet Core Central on YouTube](https://www.youtube.com/watch?v=w84uFSwulBI&feature=emb_rel_end)
- [Codeburst on Medium](https://codeburst.io/get-started-with-rabbitmq-2-consume-messages-using-hosted-service-e7e6a20b15a6)

## Application Breakdown

### .NET

- `RabbitMQ.SignalRConsumer` - Consumer of queue via SignalR Connection
- `RabbitMQ.WebApi` - SignalR connection and API endpoint to proxy RabbitMQ for consuming applications
- `RabbitMQ.Consumer` - Direct consumer of RabbitMQ queue
- `RabbitMQ.Producer` - Direct producer of messages to RabbitMQ
- `RabbitMQ.Lib` - .NET Class Lib for shared functionality around RabbitMQ

### Node.js

- `Node/signalr-consumer` - Node.js consumer of queue via SignalR Connection