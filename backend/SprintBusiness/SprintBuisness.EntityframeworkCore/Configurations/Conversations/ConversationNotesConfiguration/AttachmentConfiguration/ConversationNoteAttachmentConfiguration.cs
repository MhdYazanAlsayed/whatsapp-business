using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Conversations.Notes.Attachments;
using SprintBusiness.Domain.Conversations.Notes.Attachments.Keys;
using SprintBusiness.Domain.Conversations.Notes.Keys;

namespace SprintBuisness.EntityframeworkCore.Configurations.Conversations.ConversationNotesConfiguration.AttachmentConfiguration
{
    public class ConversationNoteAttachmentConfiguration : IEntityTypeConfiguration<ConversationNoteAttachment>
    {
        public void Configure(EntityTypeBuilder<ConversationNoteAttachment> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion(id => id.Value, src => new ConversationNoteAttachmentId(src));

            builder.Property(x => x.ConversationNoteId)
                .HasConversion(id => id.Value, src => new ConversationNoteId(src));

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.FileName)
                .HasMaxLength(255);

            builder
                .Property(x => x.FileId)
                .HasMaxLength(255);

            builder
                .HasOne(x => x.ConversationNote)
                .WithMany(x => x.Attachments)
                .HasForeignKey(x => x.ConversationNoteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
