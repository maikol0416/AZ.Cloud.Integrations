using System;

namespace ServiceBus.HandlerAzureServiceBus
{
	public interface IServicesBusHandler
	{
        /// <summary>
        /// Send message queue
        /// </summary>
        /// <param name="message">Message Data</param>
        /// <param name="queue">Queue name</param>
        /// <returns></returns>
        Task SendMessageQueue(EventQueue message, string queue);
    }
}

