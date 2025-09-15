using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Templates.TemplateComponents;

namespace SprintBuisness.EntityframeworkCore.Configurations.Templates.TemplateComponents
{
    public class TemplateComponentConfiguration : IEntityTypeConfiguration<TemplateComponent>
    {
        public void Configure(EntityTypeBuilder<TemplateComponent> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion(id => id.Value, x => new(x));

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Type)
                .HasMaxLength(250);

            builder.Property(x => x.Text)
                .HasMaxLength(250);

            builder.Property(x => x.Format)
               .HasMaxLength(250);

            builder.Property(x => x.TemplateId)
                .HasConversion(id => id.Value, x => new(x));

            builder.HasOne(x => x.Template)
                .WithMany(x => x.Components)
                .HasForeignKey(x => x.TemplateId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
