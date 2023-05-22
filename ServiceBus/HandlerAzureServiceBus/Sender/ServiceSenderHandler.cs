using System;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace ServiceBus.HandlerAzureServiceBus
{
	public class ServiceSenderHandler: IServicesBusHandler
    {
        ServiceBusClient client;

        ServiceBusSender sender;

        private readonly IConfiguration _configuration;

        public ServiceSenderHandler(IConfiguration configuration)
		{
            _configuration = configuration;
            InitialSettings();
        }

        /// <summary>
        /// Initial configurations
        /// </summary>
        private void InitialSettings()
        {
            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            client = new ServiceBusClient(_configuration["ServicesBus:send"], clientOptions);
        }

        /// <summary>
        /// Send message queue
        /// </summary>
        /// <param name="message">Message Data</param>
        /// <param name="queue">Queue name</param>
        /// <returns></returns>
        public async Task SendMessageQueue(EventQueue message,string queue)
        {
            sender = client.CreateSender(queue);
            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            messageBatch.TryAddMessage(new ServiceBusMessage(JsonSerializer.Serialize(message)));
            try
            {
                await sender.SendMessagesAsync(messageBatch);
            }
            finally
            {
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }
	}
}

