using MediatR;
using SprintBuisness.Contracts.Services;

namespace SprintBusiness.Features.ConversationNotes.Commands.UploadAttachments
{
    public class UploadNoteAttachmentCommandHandler : IRequestHandler<UploadNoteAttachmentCommand, UploadNoteAttachmentResult>
    {
        private readonly IFileManager _fileManager;

        public UploadNoteAttachmentCommandHandler(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public async Task<UploadNoteAttachmentResult> Handle(UploadNoteAttachmentCommand request, CancellationToken cancellationToken)
        {
            var result = new UploadNoteAttachmentResult();

            foreach (var file in request.Files)
            {
                var id = await _fileManager.SaveAsync("ConversationNoteAttachments" , file);

                result.Attachments.Add(new()
                {
                    FileName = file.FileName,
                    FileId = id
                });
            }

            return result;
        }
    }
}
