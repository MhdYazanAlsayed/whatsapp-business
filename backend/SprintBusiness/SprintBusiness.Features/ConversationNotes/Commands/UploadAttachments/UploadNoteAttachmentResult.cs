namespace SprintBusiness.Features.ConversationNotes.Commands.UploadAttachments
{
    public class UploadNoteAttachmentResult
    {
        public List<UploadNoteAttachmentItemResult> Attachments { get; set; } = new();
    }

    public class UploadNoteAttachmentItemResult
    {
        public string FileName { get; set; } = null!;
        public string FileId { get; set; } = null!;
    }
}
