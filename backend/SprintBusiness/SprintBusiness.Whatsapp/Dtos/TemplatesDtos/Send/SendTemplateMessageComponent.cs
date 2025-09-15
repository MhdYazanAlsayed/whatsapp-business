using SprintBusiness.Domain.Templates.TemplateComponents.Enums;

namespace SprintBusiness.Whatsapp.Dtos.TemplatesDtos.Send
{
    public class SendTemplateMessageComponent
    {
        public required TemplateComponentType Type { get; set; }
        public required List<SendTemplateMessageParametersDto> Parameters { get; set; }
    }
}
