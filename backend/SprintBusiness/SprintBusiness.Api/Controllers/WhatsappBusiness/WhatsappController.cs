using MediatR;
using Microsoft.AspNetCore.Mvc;
using SprintBusiness.Api.Requests;
using SprintBusiness.Features.Whatsapp.Webhooks.ReceiveMessage;
using System.Text.Json;

namespace SprintBusiness.Api.Controllers.WhatsappBusiness
{
    [ApiController]
    [Route("api/bot/webhooks")]
    public class WhatsappController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<WhatsappController> _logger;

        public WhatsappController(IMediator mediator , ILogger<WhatsappController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Webhook(WebhookTestRequest request)
        {
            return Ok(request.Challange);
        }

        [HttpPost]
        public async Task<IActionResult> WebhookAsync([FromBody] ReceiveMessageCommand request)
        {
            try
            {
                _logger.LogInformation("Webhook request :\n" + JsonSerializer.Serialize(request));

                await _mediator.Send(request);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
