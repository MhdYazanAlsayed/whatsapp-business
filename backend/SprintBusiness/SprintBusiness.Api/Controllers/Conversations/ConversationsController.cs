using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Client;
using SprintBusiness.Api.Requests;
using SprintBusiness.Api.Requests.Conversations;
using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Features.Conversations.Commands.ConvertConversation;
using SprintBusiness.Features.Conversations.Commands.SaveNote;
using SprintBusiness.Features.Conversations.Commands.SaveNote.Dtos;
using SprintBusiness.Features.Conversations.Commands.SendMessage;
using SprintBusiness.Features.Conversations.Commands.TakeConversation;
using SprintBusiness.Features.Conversations.Queries.DetailsAsync;
using SprintBusiness.Features.Conversations.Queries.FetchMessages;
using SprintBusiness.Features.Conversations.Queries.GetConvertOptions;
using SprintBusiness.Features.Conversations.Queries.GetPaginationAsync;
using SprintBusiness.Features.Conversations.Queries.GetWorkGroupConversations;
using SprintBusiness.Features.Templates.Commands.SendTemplate;
using SprintBusiness.Features.Templates.Commands.SendTemplate.Dtos;

namespace SprintBusiness.Api.Controllers.Conversations
{
    [ApiController]
    [Authorize]
    [Route("api/conversations")]
    public class ConversationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ConversationsController> _logger;

        public ConversationsController(IMediator mediator , ILogger<ConversationsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("bot")]
        public async Task<IActionResult> BotConversationsAsync([FromQuery, BindRequired] int page)
        {
            return Ok(await _mediator.Send(new GetConversationsPaginationQuery(ConversationOwner.Bot, page)));
        }

        [HttpGet("customer-service")]
        public async Task<IActionResult> CustomerServiceConversationsAsync([FromQuery, BindRequired] int page)
        {
            return Ok(await _mediator.Send(new GetConversationsPaginationQuery(ConversationOwner.CustomerService, page)));
        }

        [HttpGet("workgroup/{id}")]
        public async Task<IActionResult> GetWorkGroupConversationsAsync(int id, [FromQuery] GetWorkGroupCovnersationsRequest request)
        {
            var response = await _mediator
                    .Send(new GetWorkGroupConversationsQuery(id, request.Page));

            return Ok(response);
        }

        [HttpGet("{id}/convert-options")]
        public async Task<IActionResult> GetConvertOptionsAsync(int id)
        {
            return Ok(await _mediator.Send(new GetConvertOptionsQuery(id)));
        }

        [HttpPost("{id}/convert")]
        public async Task<IActionResult> ConvertAsync(int id, [FromBody] ConvertConversationRequest request)
        {
            await _mediator.Send(new ConvertConversationCommand(
                id,
                request.To,
                request.EmployeeId,
                request.WorkGroupId));

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery, BindRequired] int page)
        {
            return Ok(await _mediator.Send(
                new GetConversationsPaginationQuery(ConversationOwner.User, page)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DetailsAsync(int id)
        {
            return Ok(await _mediator.Send(new GetConvertsationDetailsQuery(id)));
        }

        [HttpPost("{id}/take")]
        public async Task<IActionResult> TakeAsync(int id)
        {
            var result = await _mediator.Send(new TakeConversationCommand(id));

            if (!result.Succeeded)
                return BadRequest(result.Message);

            return Ok();
        }

        [HttpGet("{id}/messages")]
        public async Task<IActionResult> GetMessagesAsync([FromQuery, BindRequired] int page, int id)
        {
            return Ok(await _mediator.Send(new FetchMessagesQuery(id, page)));
        }

        [HttpPost("{id}/send-message")]
        public async Task<IActionResult> SendMessageAsync(int id, [FromBody] SendMessageRequest request)
        {
            try
            {
                await _mediator.Send(new SendMessageCommand(id, request.Content));

                return Ok();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error while sending message");
                return BadRequest("Error while sending message : " + ex.Message);
            }
        }

        [HttpPost("{id}/send-template")]
        public async Task<IActionResult> SendTemplateAsync(int id, [FromBody] SendTemplateDto dto)
        {
            var result = await _mediator.Send(new SendTemplateCommand(id, dto));

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("{id}/note")]
        public async Task<IActionResult> SaveNoteAsync(int id ,[FromBody] SaveConversationNoteDto dto)
        {
            var result = await _mediator.Send(new SaveConversationNoteCommand(id , dto));

            return Ok(result);
        }
    }
}
