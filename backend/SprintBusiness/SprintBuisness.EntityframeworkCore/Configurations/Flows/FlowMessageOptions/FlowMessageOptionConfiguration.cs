using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Flows.FlowMessageItems;
using SprintBusiness.Domain.Flows.FlowMessageItems.Keys;
using SprintBusiness.Domain.Flows.FlowMessages.Keys;

namespace SprintBuisness.EntityframeworkCore.Configurations.Flows.FlowMessageOptions
{
    public class FlowMessageOptionConfiguration : IEntityTypeConfiguration<FlowMessageOption>
    {
        public void Configure(EntityTypeBuilder<FlowMessageOption> builder)
        {
            builder
                .Property(x => x.Id)
                .HasConversion(id => id.Value, value => new FlowMessageOptionId(value));

            builder
                .Property(x => x.FlowMessageId)
                .HasConversion(id => id.Value, value => new FlowMessageId(value));

            builder
                .Property(x => x.Next)
                .HasConversion(next => next.Value, value => new FlowMessageId(value));

            builder
                .HasOne(x => x.FlowMessage)
                .WithMany(x => x.Options)
                .HasForeignKey(x => x.FlowMessageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(x => x.Number)
                .UseIdentityColumn(1, 1);
        }
    }
}
