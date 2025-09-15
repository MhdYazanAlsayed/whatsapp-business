using MediatR;
using Microsoft.AspNetCore.Mvc;
using SprintBusiness.Api.Requests.WorkGroups;
using SprintBusiness.Features.WorkGroups.Commands.Create;
using SprintBusiness.Features.WorkGroups.Commands.Delete;
using SprintBusiness.Features.WorkGroups.Commands.Edit;
using SprintBusiness.Features.WorkGroups.Queries.Details;
using SprintBusiness.Features.WorkGroups.Queries.GetMembers;
using SprintBusiness.Features.WorkGroups.Queries.GetWorkGroups;
using SprintBusiness.Features.WorkGroups.Queries.GetWorkGroupsPagination;

namespace SprintBusiness.Api.Controllers
{
    [ApiController]
    [Route("api/work-groups")]
    public class WorkGroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkGroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] string? keyword)
        {
            return Ok(await _mediator.Send(new GetWorkGroupsQuery(keyword)));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetPaginationAsync([FromQuery] GetWorkGroupPaginationRequest request)
        {
            return Ok(await _mediator.Send(
                new GetWorkGroupsPaginationQuery(request.Page , request.Keyword)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] WorkGroupRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _mediator.Send(new CreateWorkGroupCommand(request.Name));

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DetailsAsync (int id)
        {
            return Ok(await _mediator.Send(new DetailsWorkGroupQuery(id)));
        }

        [HttpGet("{id}/members")]
        public async Task<IActionResult> GetMembersAsync(int id)
        {
            return Ok(await _mediator.Send(new GetWorkGroupMembersQuery(id)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync (int id , [FromBody] WorkGroupRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _mediator.Send(new EditWorkGroupCommand(id , request.Name));

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync (int id)
        {
            var result = await _mediator.Send(new DeleteWorkGroupCommand(id));

            if (!result.Succeeded) 
                return BadRequest(result.Message);

            return Ok();
        }

    }
}
