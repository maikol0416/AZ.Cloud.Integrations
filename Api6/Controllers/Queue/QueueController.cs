using Api.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceBus.HandlerAzureServiceBus;
using Util.Common;
using Utilidades;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api6.Controllers
{
    [Route(Constants.UriForDefaultWebApi + "[controller]")]
    [ApiController]

    public class QueueController : HandlerBaseLiteController<EventQueue>
    {
        private readonly IServicesBusHandler _servicesBusHandler;

        public QueueController(IMediator mediator,IServicesBusHandler servicesBusHandler)
        {
            _servicesBusHandler = servicesBusHandler;
        }

        [HttpPost("sendEventQueue")]
        public async Task<IActionResult> SendEventHP(QueueInputDto eventQueue)
        {
            await _servicesBusHandler.SendMessageQueue(new EventQueue() { Data = "{data test queu}" },"NameQueue");
            return HandlerResponse(eventQueue);
        }
    }


}

