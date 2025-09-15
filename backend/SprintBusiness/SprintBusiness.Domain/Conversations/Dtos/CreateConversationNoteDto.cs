using SprintBusiness.Domain.Conversations.Notes.Dtos;

namespace SprintBusiness.Domain.Conversations.Dtos
{
    public class CreateConversationNoteDto
    {
        public CreateConversationNoteDto(string content , List<ConversationNoteAttachmentDto> attachments)
        {
            Content = content;
            Attachments = attachments;
        }

        public string Content { get; set; }
        public List<ConversationNoteAttachmentDto> Attachments { get; set; }
    }
}
