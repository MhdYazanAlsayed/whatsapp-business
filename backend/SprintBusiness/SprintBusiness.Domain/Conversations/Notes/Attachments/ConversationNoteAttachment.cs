using SprintBusiness.Domain.Conversations.Notes.Attachments.Keys;
using SprintBusiness.Domain.Conversations.Notes.Keys;

namespace SprintBusiness.Domain.Conversations.Notes.Attachments
{
    public class ConversationNoteAttachment
    {
        protected ConversationNoteAttachment()
        {
            Id = new ConversationNoteAttachmentId(0);
            FileName = string.Empty;
            FileId = string.Empty;
            ConversationNoteId = new ConversationNoteId(0);
        }

        internal ConversationNoteAttachment(ConversationNoteId id, string fileName, string fileId)
        {
            if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(fileId))
                throw new ArgumentNullException();

            FileName = fileName;
            FileId = fileId;
            ConversationNoteId = id;
        }

        public ConversationNoteAttachmentId Id { get; private set; } = null!;
        public string FileName { get; private set; }
        public string FileId { get; private set; }

        public ConversationNote? ConversationNote { get; set; }
        public ConversationNoteId ConversationNoteId { get; set; }
    }
}
