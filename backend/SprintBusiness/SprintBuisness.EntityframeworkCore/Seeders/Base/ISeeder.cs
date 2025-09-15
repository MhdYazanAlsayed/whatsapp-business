namespace SprintBuisness.EntityframeworkCore.Seeders
{
    public interface ISeeder
    {
        Task SeedAsync(IServiceProvider serviceProvider);
        int Order { get; }
    }
}
