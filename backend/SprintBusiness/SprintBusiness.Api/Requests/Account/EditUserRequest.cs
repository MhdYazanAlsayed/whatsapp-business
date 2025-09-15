using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Api.Requests.Account
{
    public class EditEmployeeDto
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string EnglishName { get; set; } = null!;

        [Required]
        public string ArabicName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;
    }
}
