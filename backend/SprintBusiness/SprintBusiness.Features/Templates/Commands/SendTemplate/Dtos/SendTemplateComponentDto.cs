using SprintBusiness.Domain.Templates.TemplateComponents.Enums;
using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Features.Templates.Commands.SendTemplate.Dtos
{
    public class SendTemplateComponentDto
    {
        [Required]
        public TemplateComponentType Type { get; set; }  

        [Required]
        public List<SendTemplateParameterDto> Parameters { get; set; } = null!;
    }
}
