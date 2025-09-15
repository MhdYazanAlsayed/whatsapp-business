using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Api.Requests.Account
{
    public class ChangePasswordRequest
    {
        public string? UserId { get; set; }

        [Required]
        public string Password { get; set; } = null!;

        [Required, Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
    }
}
