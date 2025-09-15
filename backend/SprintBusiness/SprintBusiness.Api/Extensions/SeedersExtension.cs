using System.Reflection;
using SprintBuisness.EntityframeworkCore.Seeders;

namespace SprintBusiness.Api.Extensions;

public static class SeedersExtension
{
    public static async Task RunSeedersAsync(this WebApplication app)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var seeders = assemblies.SelectMany(a => a.GetTypes())
            .Where(t => typeof(ISeeder).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .OrderBy(t => ((ISeeder)Activator.CreateInstance(t)!).Order)
            .ToList();


        var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        //var logger = serviceProvider.GetRequiredService<ILogger>();

        foreach (var seederType in seeders)
        {
            try
            {
                var seeder = (ISeeder)Activator.CreateInstance(seederType)!;
                //logger.LogInformation($"Running seeder: {seederType.Name}");

                await seeder.SeedAsync(serviceProvider);
            }
            catch (Exception ex)
            {
                //logger.LogError($"Error executing seeder {seederType.Name}: {ex.Message}");
                throw;
            }
        }
    }
}
