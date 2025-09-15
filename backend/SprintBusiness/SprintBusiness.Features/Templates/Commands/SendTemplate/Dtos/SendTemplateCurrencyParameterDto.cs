using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Features.Templates.Commands.SendTemplate.Dtos
{
    public class SendTemplateCurrencyParameterDto
    {
        [Required]
        public string Code { get; set; } = null!;

        [Required]
        public float Amount { get; set; } 
    }
}
