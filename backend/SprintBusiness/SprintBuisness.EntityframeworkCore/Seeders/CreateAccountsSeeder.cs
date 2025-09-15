using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Users;
using SprintBusiness.Domain.Users.Enums;

namespace SprintBuisness.EntityframeworkCore.Seeders
{
    public class CreateAccountsSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            //var users = new List<string>()
            //{
            //    "Leen ali",
            //    "Sadeem Asiri",
            //    "Asrar Ahmed",
            //    "Amal Alshehry",
            //    "Razan Almazni" ,
            //    "Admin"
            //};

            //foreach (var user in users)
            //{
            //    if (await context.Employees.AnyAsync(x => x.EnglishName.ToLower() == user.ToLower()))
            //        continue;

            //    var account = Employee.Create(user, user, user + "@gmail.com", UserType.User);

            //    await context.AddAsync(account);
            //}

            //await context.SaveChangesAsync();
        }
    }
}
