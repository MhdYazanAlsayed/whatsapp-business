using SprintBusiness.Domain.Base;
using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Conversations.Notes.Attachments;
using SprintBusiness.Domain.Conversations.Notes.Attachments.Keys;
using SprintBusiness.Domain.Conversations.Notes.Dtos;
using SprintBusiness.Domain.Conversations.Notes.Keys;

namespace SprintBusiness.Domain.Conversations.Notes
{
    public class ConversationNote : Entity
    {
        protected ConversationNote()
        {
            ConversationId = null!;
            Content = null!;
        }

        private ConversationNote(ConversationId conversationId , string content)
        {
            ConversationId = conversationId;
            Content = content;
        }

        public ConversationNoteId Id { get; private set; } = null!;
        public ConversationId ConversationId { get; private set; }
        public Conversation? Conversation { get; private set; }
        public string Content { get; private set; }
        public List<ConversationNoteAttachment> Attachments { get; private set; } = new();

        public List<ConversationNoteAttachment> AddAttachments (List<ConversationNoteAttachmentDto> dto)
        {
            foreach (var item in dto)
            {
                var attachment = 
                    new ConversationNoteAttachment(Id , item.FileName, item.FileId);

                Attachments.Add(attachment);    
            }

            return Attachments;
        }

        public void Update (string content)
        {
            Content = content;
        }

        public void RemoveAttachments (List<ConversationNoteAttachmentId> ids)
        {
            Attachments.RemoveAll(x => ids.Contains(x.Id));
        }

        public static ConversationNote Create (ConversationId conversationId , string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException(nameof(content));

            return new ConversationNote(conversationId , content);
        }

        // public ConversationNoteAttachment AddAttachment (ConversationNoteAttachmentDto dto)
        // {
        //     var attachment = new ConversationNoteAttachment(Id,dto.FileName, dto.FileId);

        //     Attachments.Add(attachment);

        //     return attachment;
        // }
    }
}
