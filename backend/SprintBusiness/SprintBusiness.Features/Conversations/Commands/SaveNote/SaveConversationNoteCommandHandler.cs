using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.Contracts.Services;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Conversations.Notes;
using SprintBusiness.Domain.Conversations.Notes.Attachments;
using SprintBusiness.Domain.Conversations.Notes.Attachments.Keys;
using SprintBusiness.Domain.Conversations.Notes.Dtos;

namespace SprintBusiness.Features.Conversations.Commands.SaveNote
{
    public class SaveConversationNoteCommandHandler : IRequestHandler<SaveConversationNoteCommand, ConversationNote>
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileManager _fileManager;

        public SaveConversationNoteCommandHandler(ApplicationDbContext context ,
            IFileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        public async Task<ConversationNote> Handle(SaveConversationNoteCommand request, CancellationToken cancellationToken)
        {
            var conversationNote = await _context.ConversationNotes
                .Include(x => x.Attachments)
                .SingleOrDefaultAsync(x => x.ConversationId == request.Id);
            
            // Create new note
            if (conversationNote is null)
            {
                conversationNote = ConversationNote.Create(request.Id , request.Content);

                // Add attachments
                conversationNote
                .AddAttachments(request.AttachmentsToAdd.Select(x => new ConversationNoteAttachmentDto
                {
                    FileId = x.FileId ,
                    FileName = x.FileName
                }).ToList());
                
                await _context.AddAsync(conversationNote);
                await _context.SaveChangesAsync();

                return conversationNote;
            }

            // Update note
            conversationNote.Update(request.Content);
            conversationNote
            .AddAttachments(request.AttachmentsToAdd.Select(x => new ConversationNoteAttachmentDto
            {
                FileId = x.FileId ,
                FileName = x.FileName
            }).ToList());

            // Remove attachments

            var attachmentsToDelete = conversationNote.Attachments
            .Where(x => request.AttachmentsToDelete.Any(y => y.AttachmentId == x.Id.Value))
            .ToList();   

            conversationNote.RemoveAttachments(
                request.AttachmentsToDelete
                .Select(x => new ConversationNoteAttachmentId(x.AttachmentId))
                .ToList()
            );

            _context.Update(conversationNote);
            await _context.SaveChangesAsync();

            DeleteAttachmentFiles(attachmentsToDelete);

            return conversationNote;
        }

        private void DeleteAttachmentFiles (List<ConversationNoteAttachment> attachments)
        {
            if (attachments.Count == 0) return;

            foreach (var attachment in attachments)
            {
                _fileManager.Delete("ConversationNoteAttachments", attachment.FileId);

                _context.Remove(attachment);
            }
        }

    }
}
