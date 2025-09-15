using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Flows.FlowMessages;
using SprintBusiness.Domain.Flows.FlowMessages.Keys;
using SprintBusiness.Domain.Flows.Keys;

namespace SprintBuisness.EntityframeworkCore.Configurations.Flows.FlowMessages
{
    public class FlowMessageConfiguration : IEntityTypeConfiguration<FlowMessage>
    {
        public void Configure(EntityTypeBuilder<FlowMessage> builder)
        {
            builder
                .Property(x => x.Id)
                .HasConversion(id => id.Value, value => new FlowMessageId(value));

            builder
                .Property(x => x.FlowId)
                  .HasConversion(
                    id => id != null ? id.Value : (int?)null, 
                        value => value.HasValue ? new FlowId(value.Value) : null  
                    );

            builder
                .Property(x => x.ButtonListDisplayText)
                .HasMaxLength(20);

            builder
                .HasOne(x => x.Flow)
                .WithMany()
                .HasForeignKey(x => x.FlowId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
