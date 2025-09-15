using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Features.Templates.Commands.SendTemplate.Dtos
{
    public class SendTemplateFileParameterDto
    {
        [Required]
        public string FileName { get; set; } = null!;
    }
}
