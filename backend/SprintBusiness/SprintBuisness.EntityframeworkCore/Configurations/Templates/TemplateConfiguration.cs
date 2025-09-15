using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Templates;

namespace SprintBuisness.EntityframeworkCore.Configurations.Templates
{
    public class TemplateConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion(id => id.Value, x => new(x));

            builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETDATE()");   

            builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

            builder.Property(x => x.Status)
                .HasMaxLength(50);

            builder.Property(x => x.Category)
                .HasMaxLength(250);

            builder.Property(x => x.SubCategory)
                .HasMaxLength(250);

            builder.Property(x => x.Language)
                .HasMaxLength(25);

            builder
            .Property(x => x.TemplateId)
            .HasMaxLength(50);
        }
    }
}
