using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Templates.TemplateVariables;

namespace SprintBuisness.EntityframeworkCore.Configurations.Templates.TemplateComponents.TemplateVariables
{
    public class TemplateVariableConfiguration : IEntityTypeConfiguration<TemplateVariable>
    {
        public void Configure(EntityTypeBuilder<TemplateVariable> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion(id => id.Value, x => new(x));

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Key)
                .HasMaxLength(250);

            builder.Property(x => x.Value)
                .HasMaxLength(1000);

            builder.Property(x => x.ComponentId)
                .HasConversion(id => id.Value, x => new(x));

            builder
                .HasOne(x => x.Component)
                .WithMany(x => x.Variables)
                .HasForeignKey(x => x.ComponentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
