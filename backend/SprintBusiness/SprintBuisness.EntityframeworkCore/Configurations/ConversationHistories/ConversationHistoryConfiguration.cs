using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Conversations.Histories;
using SprintBusiness.Domain.Conversations.Keys;

namespace SprintBuisness.EntityframeworkCore.Configurations.CustomerServiceConversationHistories
{
    public class ConversationHistoryConfiguration : IEntityTypeConfiguration<ConversationHistory>
    {
        public void Configure(EntityTypeBuilder<ConversationHistory> builder)
        {
            builder
                .Property(x => x.Id)
                .HasConversion(id => id.Value, value => new ConversationHistoryId(value));

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.ConversationId)
                .HasConversion(id => id.Value, value => new ConversationId(value));

            builder
                .HasOne(x => x.Conversation)
                .WithMany(x => x.Histories)
                .HasForeignKey(X => X.ConversationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Employee)
                .WithMany(x => x.ConversationHistories)
                .HasForeignKey(X => X.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.WorkGroup)
                .WithMany()
                .HasForeignKey(X => X.WorkGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
