using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Messages.Templates;
using SprintBusiness.Domain.Messages.Templates.Keys;

namespace SprintBuisness.EntityframeworkCore.Configurations.Messages.Templates
{
    public class TemplateMessageConfigurations : IEntityTypeConfiguration<TemplateMessage>
    {
        public void Configure(EntityTypeBuilder<TemplateMessage> builder)
        {
            builder
                 .Property(x => x.Id)
                 .HasConversion(id => id.Value, value => new TemplateMessageId(value));

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.HeaderFileName)
                .HasMaxLength(255);

            builder
                .Property(x => x.Body)
                .HasMaxLength(500);

            builder
                .Property(x => x.Footer)
                .HasMaxLength(500);
        }
    }
}
