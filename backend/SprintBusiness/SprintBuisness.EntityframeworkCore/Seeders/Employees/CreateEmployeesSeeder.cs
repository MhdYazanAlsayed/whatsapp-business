using Microsoft.Extensions.DependencyInjection;
using SprintBuisness.EntityframeworkCore.Contexts;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using SprintBusiness.Domain.Users;
using System.Text.Json;
using SprintBuisness.EntityframeworkCore.Seeders.Employees;
using SprintBusiness.Domain.Users.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace SprintBuisness.EntityframeworkCore.Seeders
{
    public class CreateEmployeesSeeder : ISeeder
    {
        public int Order => 1;

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            // throw new NotImplementedException();    
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            if (await context.Employees.AnyAsync(x => x.Type == UserType.SuperAdmin))
                return;
            
            // Create an HTTP client directly
            using var httpClient = new HttpClient();
            
            // Set the base address from configuration
            var apiBaseUrl = configuration.GetValue<string>("OAuthConfigurations:Url") ?? "https://localhost:7064/";
            httpClient.BaseAddress = new Uri(apiBaseUrl);
            
            // Sample employee data to create
            var employeeData = new
            {
                UserName = "Superadmin",
                Password = "MyP@ssw0rd",
                ConfirmPassword = "MyP@ssw0rd"
            };
            
            try
            {
                // Call the endpoint to create the employee if it doesn't exist
                var response = await httpClient.PostAsJsonAsync($"api/auth/{employeeData.UserName}/create", employeeData);
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Employee creation successful: {result}");

                    var createEmployeeResult = JsonConvert.DeserializeObject<UserResponseDto>(result);
                    if (createEmployeeResult is null) 
                    {
                        throw new Exception("Failed to deserialize employee creation result");
                    }
                    // Create the user 
                    await context.Employees.AddAsync(Employee.Create(
                        createEmployeeResult.Number,
                        createEmployeeResult.Id ,
                        "مدير النظام",
                        "System Administrator" ,
                        createEmployeeResult.Email ?? string.Empty ,
                        UserType.SuperAdmin ,
                        true));

                    await context.SaveChangesAsync();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to create employee: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating employee: {ex.Message}");
            }
        }
    }
}
