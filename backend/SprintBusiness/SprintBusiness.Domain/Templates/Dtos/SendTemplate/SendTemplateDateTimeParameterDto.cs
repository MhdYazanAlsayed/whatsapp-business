using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Domain.Templates.Dtos.SendTemplate
{
    public class SendTemplateDateTimeParameterDto
    {
        [Required]
        public DateTime FallBack { get; set; }
    }
}
