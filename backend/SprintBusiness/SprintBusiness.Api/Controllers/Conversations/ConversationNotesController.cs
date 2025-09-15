using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SprintBusiness.Features.ConversationNotes.Commands.UploadAttachments;

namespace SprintBusiness.Api.Controllers.Conversations
{
    [ApiController]
    [Authorize]
    [Route("api/conversations/notes/attachments")]
    public class ConversationNotesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConversationNotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync([FromForm] UploadNoteAttachmentDto dto)
        {
            var result = await _mediator.Send(new UploadNoteAttachmentCommand(dto.Files));

            return Ok(result);
        }
    }
}
