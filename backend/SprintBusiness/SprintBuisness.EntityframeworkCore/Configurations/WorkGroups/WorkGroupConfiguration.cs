using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Users.Keys;
using SprintBusiness.Domain.Users.WorkGroups;

namespace SprintBuisness.EntityframeworkCore.Configurations.WorkGroups
{
    public class WorkGroupConfiguration : IEntityTypeConfiguration<WorkGroup>
    {
        public void Configure(EntityTypeBuilder<WorkGroup> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion(id => id.Value, value => new WorkGroupId(value));

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasMaxLength(255);

            builder
                .HasMany(x => x.Employees)
                .WithMany(x => x.WorkGroups);
        }
    }
}
