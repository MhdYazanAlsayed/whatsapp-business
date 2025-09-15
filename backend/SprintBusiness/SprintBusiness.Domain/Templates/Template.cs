using SprintBusiness.Domain.Base;
using SprintBusiness.Domain.Base.Interfaces;
using SprintBusiness.Domain.Templates.Enums;
using SprintBusiness.Domain.Templates.Keys;
using SprintBusiness.Domain.Templates.TemplateButtons;
using SprintBusiness.Domain.Templates.TemplateComponents;
using SprintBusiness.Domain.Templates.TemplateComponents.Enums;

namespace SprintBusiness.Domain.Templates
{
    public class Template : Entity , ITrackableTime
    {
        protected Template()
        {
            CreatedAt = DateTime.Now;
            Name = null!;
            SubCategory = null!;
            TemplateId = null!;
        }

        private Template(string templateId , string name , TemplateStatus status , TemplateCategory category , string? subCategory , TemplateLanguage language)
        {
            TemplateId = templateId;
            Name = name;
            Status = status;
            Category = category;
            SubCategory = subCategory;
            Language = language;
        }

        public TemplateId Id { get; private set; } = null!;

        public string TemplateId { get; private set; }
        public string Name { get; private set; }
        public TemplateStatus Status { get; private set; }
        public TemplateCategory Category { get; private set; }
        public string? SubCategory { get; private set; }
        public TemplateLanguage Language { get; private set; }
        public List<TemplateComponent> Components { get; private set; } = new();
        public List<TemplateButton> Buttons { get; private set; } = new();

        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public static Template Create (string templateId , string name , TemplateStatus status, TemplateCategory category, string? subCategory, TemplateLanguage language)
        {
            return new Template(templateId , name , status, category, subCategory, language);
        }

        public TemplateButton AddButton (string? url , TemplateButtonType type , string? text)
        {
            var button = new TemplateButton(url, type, text);

            Buttons.Add(button);

            return button;
        }

        public TemplateComponent AddComponent (TemplateComponentType type , 
        TemplateComponentFormat format , string? text)
        {
            var component = new TemplateComponent(type, text, format);  

            Components.Add(component);

            return component;
        }

        public void Update (string templateId, TemplateStatus status, TemplateCategory category, string? subCategory, TemplateLanguage language)
        {
            TemplateId = templateId;
            Status = status;
            Category = category;
            SubCategory = subCategory;
            Language = language;
            UpdatedAt = DateTime.Now;
        }

        public void UpdateComponents(List<TemplateComponent> components)
        {
            Components = components;
            UpdatedAt = DateTime.Now;
        }

        public void UpdateButtons(List<TemplateButton> buttons)
        {
            Buttons = buttons;
            UpdatedAt = DateTime.Now;
        }
    }
}
