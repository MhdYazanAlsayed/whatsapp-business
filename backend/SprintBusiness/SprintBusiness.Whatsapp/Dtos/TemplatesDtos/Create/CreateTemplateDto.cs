using SprintBusiness.Domain.Templates.Enums;
using SprintBusiness.Domain.Templates.TemplateComponents.Enums;

namespace SprintBusiness.Whatsapp.Dtos.TemplatesDtos.Create
{
    // public class CreateTemplateDto
    // {
    //     public string Name { get; set; } = null!;
    //     public TemplateLanguage Language { get; set; }  
    //     public TemplateCategory Category { get; set; }  
    //     public List<TemplateComponentDto> Components { get; set; } = null!;
    // }

    // public class TemplateComponentDto
    // {
    //     public TemplateComponentType Type { get; set; }  
    //     public TemplateComponentFormat? Format { get; set; }  
    //     public string? Text { get; set; }  
    //     public List<TemplateComponentExampleDto>? Example { get; set; }
    //     public List<TemplateButtonDto>? Buttons { get; set; }
    // }

    public class CreateTemplateDto 
    {
        public string Name { get; set; } = null!;
        public TemplateLanguage Language { get; set; }  
        public TemplateCategory Category { get; set; }  
        public CreateTemplateComponent? Header { get; set; }
        public CreateTemplateComponent Body { get; set; } 
        public CreateTemplateComponent? Footer { get; set; }
        public CreateTemplateComponent? Buttons { get; set; }
    }

    public class CreateTemplateComponent 
    {
        public TemplateComponentType Type { get; set; }
        public TemplateComponentFormat? Format { get; set; }
        public string? Text { get; set; }  
        public TemplateComponentExampleDto? Example { get; set; } 
        public List<TemplateButtonDto>? Buttons { get; set; }
    }

    public class TemplateComponentExampleDto
    {
        public string? HeaderText { get; set; }
        public string? HeaderHandle { get; set; }
        public List<string>? BodyText { get; set; }
        public List<string>? FooterText { get; set; }
    }

    public class TemplateButtonDto
    {
        public TemplateButtonType Type { get; set; }
        public string? Text { get; set; }
        public string? Url { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
