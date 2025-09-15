using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Features.Conversations.Commands.SaveNote.Dtos
{
    public class SaveConversationNoteDto
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public List<SaveConversationNoteAttachmentToDeleteDto> AttachmentsToDelete { get; set; }

        [Required]
        public List<SaveConversationNoteAttachmentToAddDto> AttachmentsToAdd { get; set; }

    }
}
