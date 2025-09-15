using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Conversations.Notes;
using SprintBusiness.Domain.Conversations.Notes.Keys;

namespace SprintBuisness.EntityframeworkCore.Configurations.Conversations.ConversationNotesConfiguration
{
    public class ConversationNoteConfiguration : IEntityTypeConfiguration<ConversationNote>
    {
        public void Configure(EntityTypeBuilder<ConversationNote> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion(id => id.Value, src => new ConversationNoteId(src));

            builder.Property(x => x.ConversationId)
                .HasConversion(id => id.Value, src => new ConversationId(src));

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Content)
                .HasMaxLength(1000);

            builder
                .HasOne(x => x.Conversation)
                .WithOne(x => x.Note)
                .HasForeignKey<ConversationNote>(x => x.ConversationId)
                .IsRequired();
        }
    }
}
