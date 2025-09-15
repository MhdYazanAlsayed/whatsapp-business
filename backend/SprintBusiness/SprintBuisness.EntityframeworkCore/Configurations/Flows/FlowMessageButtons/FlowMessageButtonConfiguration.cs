using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Flows.FlowMessageButtons;
using SprintBusiness.Domain.Flows.FlowMessageButtons.Keys;
using SprintBusiness.Domain.Flows.FlowMessages.Keys;

namespace SprintBuisness.EntityframeworkCore.Configurations.Flows.FlowMessageButtons
{
    public class FlowMessageButtonConfiguration : IEntityTypeConfiguration<FlowMessageButton>
    {
        public void Configure(EntityTypeBuilder<FlowMessageButton> builder)
        {
            builder
                .Property(x => x.Id)
                .HasConversion(id => id.Value, value => new FlowMessageButtonId(value));

            builder
                .Property(x => x.FlowMessageId)
                .HasConversion(id => id.Value, value => new FlowMessageId(value));

            builder
                .Property(x => x.Next)
                .HasConversion(next => next.Value, value => new FlowMessageId(value));

            builder
                .Property(x => x.DisplayText)
                .HasMaxLength(20);

            builder
                .HasOne(x => x.FlowMessage)
                .WithMany(x => x.Buttons)
                .HasForeignKey(x => x.FlowMessageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
