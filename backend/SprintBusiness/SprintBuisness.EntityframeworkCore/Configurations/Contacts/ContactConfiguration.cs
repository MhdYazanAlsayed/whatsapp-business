using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Contacts;
using SprintBusiness.Domain.Contacts.Keys;

namespace SprintBuisness.EntityframeworkCore.Configurations.Contacts
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder
                .Property(x => x.Id)
                .HasConversion(id => id.Value , value => new ContactId(value));

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.FullName)
                .HasMaxLength(200);

            builder.Property(x => x.NickName)
                .HasMaxLength(200);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20);
        }
    }
}
