using SprintBusiness.Domain.Base;
using SprintBusiness.Domain.Templates.TemplateComponents;
using SprintBusiness.Domain.Templates.TemplateComponents.Keys;
using SprintBusiness.Domain.Templates.TemplateVariables.Keys;

namespace SprintBusiness.Domain.Templates.TemplateVariables
{
    public class TemplateVariable : Entity
    {
        protected TemplateVariable() { }
        internal TemplateVariable(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value)) 
                throw new ArgumentNullException();

            Key = key;
            Value = value;
        }

        public TemplateVariableId Id { get; private set; } = null!;
        public string? Key { get; private set; }
        public string? Value { get; private set; }
        public TemplateComponent? Component { get; private set; }
        public TemplateComponentId ComponentId { get; private set; } = null!;
    }
}
