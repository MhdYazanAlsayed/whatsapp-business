using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SprintBusiness.Domain.Users;

namespace SprintBuisness.EntityframeworkCore.Configurations.Users
{
    public class EmployeesConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Id)
            .ValueGeneratedNever();
        }
    }
}
