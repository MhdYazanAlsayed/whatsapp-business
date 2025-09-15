
using Microsoft.AspNetCore.Identity;
using SprintBusiness.Shared.Dtos;
using SprintBuisness.Contracts.Markers;
using SprintBusiness.Domain.Users;

namespace SprintBuisness.Contracts.Authentication
{
    public interface IUserService : IScopedDependency
    {
        Task<IdentityResult> RegisterAsync(Employee user, string password);
        Task<ResultDto<LoginResult>> LoginAsync(LoginRequest request);
        Task<IdentityResult> UpdateAsync(Employee user);
        string HashPassword(Employee user, string password);
    }
}
