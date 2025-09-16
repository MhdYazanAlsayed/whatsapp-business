using Microsoft.IdentityModel.Tokens;
using SprintBuisness.Contracts.Authentication;
using SprintBuisness.Contracts.Authentication.Dtos;
using SprintBusiness.Shared.Configurations;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SprintBuisness.Application.Authentication
{
    public class TokenService(
        AuthorizationConfiguration _authorizationSettings) : IToken
    {
        public async Task<ResultDto<TokenResult>> GenarateNewAccessTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(_authorizationSettings.Key);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = _authorizationSettings.ValidateIssuer,
                ValidateAudience = _authorizationSettings.ValidateAudience,
                ValidateLifetime = _authorizationSettings.RequireExpirationTime,
                RequireExpirationTime = _authorizationSettings.RequireExpirationTime,
                ValidateIssuerSigningKey = _authorizationSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public bool IsTokenExpired(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(token);
            return jwtToken.ValidTo < DateTimeCulture.Now;
        }

        public string? GetClaimValue(string accessToken, string claimType)
        {
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();

                if (!jwtHandler.CanReadToken(accessToken))
                    return null;

                var jwtToken = jwtHandler.ReadJwtToken(accessToken);

                var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType);

                return claim?.Value;
            }
            catch
            {
                return null;
            }
        }
    }
}
