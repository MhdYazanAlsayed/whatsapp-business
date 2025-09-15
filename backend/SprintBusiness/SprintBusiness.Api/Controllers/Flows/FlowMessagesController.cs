using MediatR;
using Microsoft.AspNetCore.Mvc;
using SprintBusiness.Features.Flows.FlowMessages.Commands.CreateFlowMessageCommand;

namespace SprintBusiness.Api.Controllers.Flows
{
    [ApiController]
    [Route("api/flows")]
    public class FlowMessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FlowMessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync ([FromBody] CreateFlowMessagesCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Succeeded) return BadRequest(result.Message);

            return Ok();
        }
    }
}
