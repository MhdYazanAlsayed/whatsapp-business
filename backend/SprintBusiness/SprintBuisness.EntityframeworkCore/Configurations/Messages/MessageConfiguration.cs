using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Flows.FlowMessages.Keys;
using SprintBusiness.Domain.Messages;
using SprintBusiness.Domain.Messages.Keys;

namespace SprintBuisness.EntityframeworkCore.Configurations.Messages
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                .Property(x => x.Id)
                .HasConversion(id => id.Value , value => new MessageId(value));

            builder
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
            .Property(x => x.FlowMessageId)
            .HasConversion(
                id => id != null ? id.Value : (Guid?)null,
                    value => value.HasValue ? new FlowMessageId(value.Value) : null
                );

            builder
                .Property(x => x.ConversationId)
                .HasConversion(id => id.Value, value => new ConversationId(value));

            builder
                .HasOne(x => x.Conversation)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.ConversationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.FlowMessage)
                .WithMany()
                .HasForeignKey(x => x.FlowMessageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(x => x.HistoryId)
                .HasConversion(id => id != null ? id.Value : (int?)null,
                 value => value.HasValue ? new ConversationHistoryId(value.Value) : null);

            builder
                .HasOne(x => x.History)
                .WithOne()
                .HasForeignKey<Message>(x => x.HistoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Sender)
                .WithMany()
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.TemplateMessage)
                .WithOne(x => x.Message)
                .HasForeignKey<Message>(x => x.TemplateMessageId);

            builder
                .Property(x => x.TemplateMessageId)
                .HasConversion(id => id != null ? id.Value : (int?)null,
                 value => value.HasValue ? new(value.Value) : null);
        }
    }
}
