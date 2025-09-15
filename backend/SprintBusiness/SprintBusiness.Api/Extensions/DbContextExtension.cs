using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;

namespace SprintBusiness.Api.Extensions
{
    public static class DbContextExtension
    {
        public static IServiceCollection AddDbContext (this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"))
                .LogTo(Console.WriteLine, LogLevel.Error);
            });

            return services;
        }
    }
}
