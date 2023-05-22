using System.Text.Json;
using System.Text.Json.Serialization;
using ServiceBus.HandlerAzureServiceBus;
using Util.Common;
using UtilNuget.Enum;

namespace Worker.initial;

public class ClientQueueExample : IHostedService
{
    private readonly ILogger<ClientQueueExample> _logger;
    private readonly IServicesListenerHandler _servicesListenerHandler;

    public ClientQueueExample(ILogger<ClientQueueExample> logger, IServicesListenerHandler servicesListenerHandler)
    {
        _logger = logger;
        _servicesListenerHandler = servicesListenerHandler;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _servicesListenerHandler.EventBusiness = ProcessMessage;
        await _servicesListenerHandler.DeQueueAuto("NameQueue"); //Nombre de la cola
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected async Task ProcessMessage(EventQueue eventQueue)
    {
        try
        {
           Console.WriteLine($"DeQueue Ready!!: {eventQueue.Data}");
            await Task.Delay(100);
           _servicesListenerHandler.DeQueue = true;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

