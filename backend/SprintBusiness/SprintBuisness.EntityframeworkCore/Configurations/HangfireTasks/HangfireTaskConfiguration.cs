using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Hangfire;

namespace SprintBuisness.EntityframeworkCore.Configurations.HangfireTasks;

public class HangfireTaskConfiguration : IEntityTypeConfiguration<HangfireTask>
{
    public void Configure(EntityTypeBuilder<HangfireTask> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Key).IsUnique();
        builder.Property(x => x.Key).IsRequired().HasMaxLength(255);
        builder.Property(x => x.TaskId).IsRequired().HasMaxLength(255);
    }
}
