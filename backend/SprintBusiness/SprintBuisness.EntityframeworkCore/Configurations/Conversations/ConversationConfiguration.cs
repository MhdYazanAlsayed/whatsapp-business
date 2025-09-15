using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Contacts.Keys;
using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Conversations.Keys;

namespace SprintBuisness.EntityframeworkCore.Configurations.Conversations
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder
                .Property(x => x.Id)
                .HasConversion(id => id.Value, value => new ConversationId(value));

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
                
            builder
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(x => x.ContactId)
                .HasConversion(id => id.Value, value => new ContactId(value));

            builder
                .HasOne(x => x.CustomerServiceEmployee)
                .WithMany(x => x.Conversations)
                .HasForeignKey(x => x.CustomerServiceEmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(x => x.WorkGroup)
                .WithMany(x => x.Conversations)
                .HasForeignKey(x => x.WorkGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Contact)
                .WithOne(x => x.Conversation)
                .HasForeignKey<Conversation>(x => x.ContactId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
