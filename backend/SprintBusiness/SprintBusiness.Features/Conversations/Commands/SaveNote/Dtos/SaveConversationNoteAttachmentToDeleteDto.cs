using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Features.Conversations.Commands.SaveNote.Dtos
{
    public class SaveConversationNoteAttachmentToDeleteDto
    {
        [Required]
        public int AttachmentId { get; set; }
    }
}
