using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.ReplyTemplates;
using SprintBusiness.Domain.ReplyTemplates.Keys;

namespace SprintBuisness.EntityframeworkCore.Configurations.ReplyTemplates
{
    public class ReplyTemplatesConfiguration : IEntityTypeConfiguration<ReplyTemplate>
    {
        public void Configure(EntityTypeBuilder<ReplyTemplate> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion(id => id.Value, value => new ReplyTemplateId(value));

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Title)
                .HasMaxLength(255);

            builder
                .Property(x => x.Content)
                .HasMaxLength(2000);

            //builder
            //    .HasOne(x => x.User)
            //    .WithMany(x => x.ReplyTemplates)
            //    .HasForeignKey(x => x.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
