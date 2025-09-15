using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Flows.FlowMessageLists;
using SprintBusiness.Domain.Flows.FlowMessageLists.Keys;
using SprintBusiness.Domain.Flows.FlowMessages.Keys;

namespace SprintBuisness.EntityframeworkCore.Configurations.Flows.FlowMessageLists
{
    public class FlowMessageListConfiguration : IEntityTypeConfiguration<FlowMessageListItem>
    {
        public void Configure(EntityTypeBuilder<FlowMessageListItem> builder)
        {
            builder
                .Property(x => x.Id)
                .HasConversion(id => id.Value, value => new FlowMessageListItemId(value));

            builder
                .Property(x => x.FlowMessageId)
                .HasConversion(id => id.Value, value => new FlowMessageId(value));

            builder
                .Property(x => x.Next)
                .HasConversion(next => next.Value, value => new FlowMessageId(value));

            builder
                .Property(x => x.Content)
                .HasMaxLength(24);

            builder
                .Property(x => x.Description)
                .HasMaxLength(72);

            builder
                .HasOne(x => x.FlowMessage)
                .WithMany(x => x.ListItems)
                .HasForeignKey(x => x.FlowMessageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
