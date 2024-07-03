using IbmmqConnect.Api.IbmMq;
using Microsoft.AspNetCore.Mvc;

namespace IbmmqConnect.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IbmMqController : ControllerBase
    {
        private readonly ILogger<IbmMqController> _logger;
        private readonly IProducer _producer;

        public IbmMqController(
            ILogger<IbmMqController> logger, 
            IProducer producer)
        {
            _logger = logger;
            _producer = producer;
        }

        [HttpPost]
        [EndpointDescription("Send a message to an IBM MQ queue")]
        public IActionResult Post(string message)
        {
            _producer.Publish("DEV.QUEUE.1", message);
            return Ok();
        }
    }
}
