using Microsoft.Extensions.Options;
using ServiceBus.HandlerAzureServiceBus;
using Util.Common;
using Worker;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        //Inyeccion de conexion con AZ Services Bus
        services.AddTransient<IServicesListenerHandler, ServicesListenerHandler>();

        //Servicio automatico para lectura de QUEUE
        services.AddHostedService<Worker.initial.ClientQueueExample>();
    })
    .Build()
    .RunAsync();

