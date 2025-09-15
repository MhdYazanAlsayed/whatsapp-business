using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SprintBusiness.Features.Templates.Commands.Create;
using SprintBusiness.Features.Templates.Commands.MigrateTemplates;
using SprintBusiness.Features.Templates.Commands.UploadMedia;
using SprintBusiness.Features.Templates.Queries.Details;
using SprintBusiness.Features.Templates.Queries.GetAllTemplates;
using SprintBusiness.Features.Templates.Queries.GetPagination;

namespace SprintBusiness.Api.Controllers.Templates
{
    [ApiController]
    [Authorize]
    [Route("api/templates")]
    public class TemplatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TemplatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync ()
        {
            return Ok(await _mediator.Send(new GetAllTemplatesQuery()));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetPaginationAsync([FromQuery] int page)
        {
            return Ok(await _mediator.Send(new GetTemplatesPaginationQuery(page)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DetailsAsync(int id)
        {
            return Ok(await _mediator.Send(new GetTemplateDetailsQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTemplateCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result.Message);
        }

        [HttpPost("sync")]
        public async Task<IActionResult> SyncAsync()
        {
            var result = await _mediator.Send(new MigrateTemplatesCommand());

            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Message);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync([FromForm] UploadTemplateMediaDto dto)
        {
            var result = await _mediator.Send(new UploadTemplateMediaCommand(dto));

            if (result.Succeeded) 
                return Ok(result.Entity);

            return BadRequest(result.Message);
        }

    }
}
