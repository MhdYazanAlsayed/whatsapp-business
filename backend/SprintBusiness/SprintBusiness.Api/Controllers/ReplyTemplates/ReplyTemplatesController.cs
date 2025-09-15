using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SprintBusiness.Features.ReplyTemplates.Commands.Create;
using SprintBusiness.Features.ReplyTemplates.Commands.Update;
using SprintBusiness.Features.ReplyTemplates.Queries.GetPagination;

namespace SprintBusiness.Api.Controllers.ReplyTemplates
{
    [ApiController]
    [Authorize]
    [Route("api/reply-templates")]
    public class ReplyTemplatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReplyTemplatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetAsync([FromQuery, BindRequired] int page)
        {
            return Ok(await _mediator
                .Send(new GetReplyTemplatesPaginationQuery(page)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateReplyTemplateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(new CreateReplyTemplateCommand(dto));
            if (!result.Succeeded)
                return BadRequest();

            return Ok(result.Entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(int id , [FromBody] UpdateReplyTemplateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = 
                await _mediator.Send(new UpdateReplyTemplateCommand(id, dto));

            if (!result.Succeeded)
                return BadRequest();

            return Ok(result.Entity);
        }
    }
}
