using System;
namespace ServiceBus.HandlerAzureServiceBus
{
	public interface IServicesListenerHandler
	{
        Func<EventQueue, Task> EventBusiness { get; set; }
        Task DeQueueAuto(string queue);
        bool DeQueue { get; set; }
    }
}

