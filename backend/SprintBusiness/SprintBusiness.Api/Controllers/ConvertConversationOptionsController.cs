using MediatR;
using Microsoft.AspNetCore.Mvc;
using SprintBusiness.Features.Conversations.Queries.GetConvertOptions;

namespace SprintBusiness.Api.Controllers
{
    [ApiController]
    [Route("api/convert-options")]
    public class ConvertConversationOptionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConvertConversationOptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] int? conversationId)
        {
            return Ok(await _mediator.Send(new GetConvertOptionsQuery(conversationId)));
        }
    }
}
