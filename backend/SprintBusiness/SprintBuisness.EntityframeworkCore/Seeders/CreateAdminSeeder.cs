using Microsoft.AspNetCore.Identity;
using SprintBusiness.Domain.Users;
using SprintBusiness.Domain.Users.Enums;

namespace SprintBuisness.EntityframeworkCore.Seeders
{
    public static class CreateAdminSeeder
    {
        public static async Task SeedAsync (UserManager<Employee> userManager)
        {
            //var admin = await userManager.FindByNameAsync("Admin");
            //if (admin is null)
            //{
            //    var user = ApplicationUser.Create("admin", "Admin", "adsa@adsad.xasmd", UserType.SuperAdmin);

            //    await userManager.CreateAsync(user, "MyP@ssw0rd");
            //}
        }
    }
}
