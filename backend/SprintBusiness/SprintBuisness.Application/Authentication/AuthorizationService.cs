using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.Contracts.Authentication;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SprintBuisness.Application.Authentication
{
    public class AuthorizationService(
        ApplicationDbContext _context,
        IHttpContextAccessor _httpContextAccessor) : IAuthorization
    {
     
        public async Task<Employee?> GetLoggedUserAsync()
        {
            throw new NotImplementedException();
            //var claims = _httpContextAccessor.HttpContext?.User;
            //if (claims is null) return null;

            //return await _userManager.GetUserAsync(claims);
        }

        public async Task<string?> GetLoggedUserIdAsync()
        {
            throw new NotImplementedException();

            //var claims = _httpContextAccessor.HttpContext?.User;
            //if (claims is null) return null;

            //return (await _userManager.GetUserAsync(claims))?.Id;
        }

        public string? GetLoggedUserId ()
        {
            var claims = _httpContextAccessor.HttpContext?.User;

            return claims?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task<Employee?> GetCurrentUser ()
        {
            var employeeId = GetCurrentEmployeeId();

            return await _context.Employees.SingleOrDefaultAsync(x => x.Id == employeeId);
        }

        public bool IsAuthenticated ()
        {
            return GetCurrentEmployeeId() != null;
        }

        public int? GetCurrentEmployeeId(string? accessToken = null)
        {
            if (accessToken is null)
            {
                var claims = _httpContextAccessor.HttpContext?.User;

                return int.TryParse(claims?.FindFirst("EmployeeId")?.Value , out var employeeId) ? employeeId : null;
            }

            var principal = GetPrincipalFromAccessToken(accessToken);
            return int.TryParse(principal.FindFirst("EmployeeId")?.Value , out var employeeId2) ? employeeId2 : null;
        }

        public async Task<Employee?> GetCurrentEmployeeAsync ()
        {
            var employeeId = GetCurrentEmployeeId();
            if (employeeId is null) return null;

            return await _context.Employees
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == employeeId);
        }
   
        // Get principal from access token
        private ClaimsPrincipal GetPrincipalFromAccessToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(accessToken);
            return new ClaimsPrincipal(new ClaimsIdentity(token.Claims));
        }   
    }
}
