using MediatR;
using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Conversations.Notes;
using SprintBusiness.Features.Conversations.Commands.SaveNote.Dtos;

namespace SprintBusiness.Features.Conversations.Commands.SaveNote
{
    public class SaveConversationNoteCommand : IRequest<ConversationNote>
    {
        public SaveConversationNoteCommand(int id, string content, List<SaveConversationNoteAttachmentToDeleteDto> delete, List<SaveConversationNoteAttachmentToAddDto> add)
        {
            Id = new(id);
            Content = content;
            AttachmentsToAdd = add;
            AttachmentsToDelete = delete;
        }

        public SaveConversationNoteCommand(int id , SaveConversationNoteDto dto)
        {
            Id = new(id);
            Content = dto.Content;
            AttachmentsToAdd = dto.AttachmentsToAdd;
            AttachmentsToDelete = dto.AttachmentsToDelete;
        }

        public ConversationId Id { get; set; }

        public string Content { get; set; }

        public List<SaveConversationNoteAttachmentToDeleteDto> AttachmentsToDelete { get; set; }
        public List<SaveConversationNoteAttachmentToAddDto> AttachmentsToAdd { get; set; }
    }
}
