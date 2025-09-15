using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Flows;
using SprintBusiness.Domain.Flows.Keys;

namespace SprintBuisness.EntityframeworkCore.Configurations.Flows
{
    public class FlowConfiguration : IEntityTypeConfiguration<Flow>
    {
        public void Configure(EntityTypeBuilder<Flow> builder)
        {
            builder
                .Property(x => x.Id)
                .HasConversion(id => id.Value, value => new FlowId(value));

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Title)
                .HasMaxLength(500);
        }
    }
}
