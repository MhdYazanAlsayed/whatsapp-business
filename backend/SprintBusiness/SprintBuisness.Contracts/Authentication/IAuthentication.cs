using SprintBuisness.Contracts.Markers;
using SprintBusiness.Domain.Users;

namespace SprintBuisness.Contracts.Authentication
{
    public interface IAuthorization : IScopedDependency
    {
        Task<Employee?> GetLoggedUserAsync();
        Task<string?> GetLoggedUserIdAsync();
        string? GetLoggedUserId();
        int? GetCurrentEmployeeId(string? accessToken = null);
        bool IsAuthenticated();
        Task<Employee?> GetCurrentUser();
        Task<Employee?> GetCurrentEmployeeAsync();
    }
}
