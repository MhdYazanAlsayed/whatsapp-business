using Microsoft.AspNetCore.Identity;

namespace SprintBusiness.Features.Account.Commands
{
    public class ValidateCommandResult
    {
        public required ValidateDataCommandResult AccessToken { get; set; }
        public required ValidateDataCommandResult RefreshToken { get; set; }
        public required IdentityUser User { get; set; }
    }

    public class ValidateDataCommandResult
    {
        public required string Token { get; set; }
        public required DateTime ExpireDate { get; set; }
    }
}
