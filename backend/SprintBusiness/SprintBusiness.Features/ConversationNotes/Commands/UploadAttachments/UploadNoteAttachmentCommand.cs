using MediatR;
using Microsoft.AspNetCore.Http;

namespace SprintBusiness.Features.ConversationNotes.Commands.UploadAttachments
{
    public class UploadNoteAttachmentCommand : IRequest<UploadNoteAttachmentResult>
    {
        public UploadNoteAttachmentCommand(IFormFile[] files)
        {
            Files = files;
        }

        public IFormFile[] Files { get; set; }
    }
}
