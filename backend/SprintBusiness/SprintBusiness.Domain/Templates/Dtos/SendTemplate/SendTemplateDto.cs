using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Domain.Templates.Dtos.SendTemplate
{
    public class SendTemplateDto
    {
        [Required]
        public string TemplateId { get; set; } = null!;

        [Required]
        public List<SendTemplateComponentDto> Components { get; set; } = null!;

    }
}
