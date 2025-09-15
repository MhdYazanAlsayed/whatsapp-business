using SprintBusiness.Domain.Base;
using SprintBusiness.Domain.Templates.Enums;
using SprintBusiness.Domain.Templates.Keys;
using SprintBusiness.Domain.Templates.TemplateButtons.Keys;

namespace SprintBusiness.Domain.Templates.TemplateButtons
{
    public class TemplateButton : Entity
    {
        protected TemplateButton() { }
        internal TemplateButton(string? url, TemplateButtonType type, string? text)
        {
            Url = url;
            Type = type;
            Text = text;
        }

        public TemplateButtonId Id { get; private set; } = null!;
        public string? Url { get; private set; }
        public TemplateButtonType Type { get; private set; }
        public string? Text { get; private set; }
        public Template? Template { get; private set; }
        public TemplateId TemplateId { get; private set; } = null!;
    }
}
