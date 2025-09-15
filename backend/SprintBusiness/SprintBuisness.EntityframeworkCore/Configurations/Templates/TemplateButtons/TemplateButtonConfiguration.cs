using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Templates.TemplateButtons;

namespace SprintBuisness.EntityframeworkCore.Configurations.Templates.TemplateButtons
{
    public class TemplateButtonConfiguration : IEntityTypeConfiguration<TemplateButton>
    {
        public void Configure(EntityTypeBuilder<TemplateButton> builder)
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

            builder.Property(x => x.Url)
                .HasMaxLength(500);

            builder.Property(x => x.TemplateId)
                .HasConversion(id => id.Value, x => new(x));

            builder.HasOne(x => x.Template)
                .WithMany(x => x.Buttons)
                .HasForeignKey(x => x.TemplateId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
