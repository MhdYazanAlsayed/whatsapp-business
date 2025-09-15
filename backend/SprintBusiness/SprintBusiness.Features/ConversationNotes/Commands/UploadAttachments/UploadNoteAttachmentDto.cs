using Microsoft.AspNetCore.Http;

namespace SprintBusiness.Features.ConversationNotes.Commands.UploadAttachments
{
    public class UploadNoteAttachmentDto
    {
        public IFormFile[] Files { get; set; } = null!;
    }
}
