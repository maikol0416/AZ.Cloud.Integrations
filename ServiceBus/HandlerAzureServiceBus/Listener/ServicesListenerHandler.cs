using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
namespace ServiceBus.HandlerAzureServiceBus
{
	public class ServicesListenerHandler:IServicesListenerHandler
    {
        ServiceBusClient client;


        public Func<EventQueue, Task> EventBusiness { get; set; }

        public bool DeQueue { get; set; }

        private readonly IConfiguration _configuration;

        public ServicesListenerHandler(IConfiguration configuration)
		{
            _configuration = configuration;
            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            client = new ServiceBusClient(_configuration["ServicesBus:listen"], clientOptions);
        }

        public async Task DeQueueAuto(string queue)
        {
            ServiceBusProcessor processor = client.CreateProcessor(queue, new ServiceBusProcessorOptions());

            processor.ProcessMessageAsync += MessageHandler;

            processor.ProcessErrorAsync += ErrorHandler;

            await processor.StartProcessingAsync();
        }


        async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            await EventBusiness(JsonSerializer.Deserialize<EventQueue>(body)??new EventQueue());
            if (DeQueue)
               await args.CompleteMessageAsync(args.Message);
            else
               await args.AbandonMessageAsync(args.Message);
        }

        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}

