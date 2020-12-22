using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Lib;
using RabbitMQ.WebApi.Hubs;
using RabbitMQ.WebApi.Services;

namespace RabbitMQ.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = "amqp://guest:guest@localhost:5672";
            var queue = "dotnet.messages.received";

            services.AddControllers();
            services.AddCors();
            services.AddSignalR();

            services.AddHostedService<MessageWorker>((sc) =>
            {
                return new MessageWorker(connectionString, queue, async (message) =>
                {
                    var messageHub = sc.GetService<IHubContext<MessageHub, IMessageHubClient>>();

                    Console.WriteLine("Received Message From Worker: " + message);
                    Console.WriteLine("Dispatching to SendAsync as OnMessageReceived");
                    await messageHub.Clients.All.OnMessageReceived(message);
                });
            });

            services.AddTransient<MessageProducer>((sc) =>
            {
                return new MessageProducer(connectionString, queue);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("/messagehub");
            });
        }
    }
}
