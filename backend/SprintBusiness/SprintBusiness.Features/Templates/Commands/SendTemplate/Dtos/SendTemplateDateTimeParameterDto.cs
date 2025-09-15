using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Features.Templates.Commands.SendTemplate.Dtos
{
    public class SendTemplateDateTimeParameterDto
    {
        [Required]
        public DateTime FallBack { get; set; }
    }
}
