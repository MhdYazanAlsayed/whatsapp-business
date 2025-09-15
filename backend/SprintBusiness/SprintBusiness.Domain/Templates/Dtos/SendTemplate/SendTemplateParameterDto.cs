using SprintBusiness.Domain.Templates.Enums;
using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Domain.Templates.Dtos.SendTemplate
{
    public class SendTemplateParameterDto
    {
        [Required]
        public TemplateParameterType Type { get; set; }
        public string? Text { get; set; }
        public SendTemplateCurrencyParameterDto? Currency { get; set; }
        public SendTemplateDateTimeParameterDto? DateTime { get; set; }
        public SendTemplateFileParameterDto? Document { get; set; }
        public SendTemplateFileParameterDto? Image { get; set; }
        public SendTemplateFileParameterDto? Video { get; set; }
        public SendTemplateFileParameterDto? Audio { get; set; }
    }

}
