using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Features.Templates.Commands.SendTemplate.Dtos
{
    public class SendTemplateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public List<SendTemplateComponentDto> Components { get; set; } = null!;

    }
}
