using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Domain.Templates.Dtos.SendTemplate
{
    public class SendTemplateCurrencyParameterDto
    {
        [Required]
        public string Code { get; set; } = null!;

        [Required]
        public float Amount { get; set; } 
    }
}
