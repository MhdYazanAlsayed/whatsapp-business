using SprintBusiness.Domain.Templates.Keys;
using SprintBusiness.Domain.Templates.TemplateComponents.Enums;
using SprintBusiness.Domain.Templates.TemplateComponents.Keys;
using SprintBusiness.Domain.Templates.TemplateVariables;

namespace SprintBusiness.Domain.Templates.TemplateComponents
{
    public class TemplateComponent
    {
        protected TemplateComponent() { }
        public TemplateComponentId Id { get; private set; } = null!;
        public TemplateComponentType Type { get; private set; }
        public string? Text { get; private set; }
        public TemplateComponentFormat Format { get; private set; }
        public TemplateId TemplateId { get; private set; } = null!;
        public Template? Template { get; private set; }
        public List<TemplateVariable> Variables { get; private set; } = new();

        internal TemplateComponent(TemplateComponentType type, string? text , TemplateComponentFormat format)
        {
            Type = type;
            Text = text;
            Format = format;
        }

        public void AddVariable (string? key , string? value)
        {
            Variables.Add(new TemplateVariable (key, value));
        }

    }
}
