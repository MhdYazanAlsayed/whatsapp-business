namespace SprintBusiness.Api.Extensions
{
    public static class IdentityExtension
    {
        public static IServiceCollection AddIdentity (this IServiceCollection services)
        {
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddUserManager<UserManager<ApplicationUser>>()
            //    .AddRoleManager<RoleManager<IdentityRole>>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();
                //.AddErrorDescriber<IdentityLocalizationDescriber>();

            return services;
        }
    }
}
