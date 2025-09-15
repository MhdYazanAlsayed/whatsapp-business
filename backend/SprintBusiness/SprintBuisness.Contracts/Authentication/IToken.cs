using SprintBuisness.Contracts.Authentication.Dtos;
using SprintBuisness.Contracts.Markers;
using SprintBusiness.Shared.Dtos;
using System.Security.Claims;

namespace SprintBuisness.Contracts.Authentication
{
    public interface IToken : IScopedDependency
    {
        Task<ResultDto<TokenResult>> GenarateNewAccessTokenAsync(string refreshToken);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        bool IsTokenExpired(string token);
        string? GetClaimValue(string accessToken, string claimType);
    }
}
