using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Domain.Templates.Dtos.SendTemplate
{
    public class SendTemplateFileParameterDto
    {
        [Required]
        public string FileName { get; set; } = null!;
    }
}
