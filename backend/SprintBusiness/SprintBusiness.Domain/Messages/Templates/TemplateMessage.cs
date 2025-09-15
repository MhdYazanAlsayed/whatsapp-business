using SprintBusiness.Domain.Base;
using SprintBusiness.Domain.Messages.Templates.Keys;

namespace SprintBusiness.Domain.Messages.Templates
{
    public class TemplateMessage : Entity
    {
        internal TemplateMessage(string? headerFileName , string? body , string? footer)
        {
            HeaderFileName = headerFileName;
            Body = body;
            Footer = footer;
        }

        public TemplateMessageId Id { get; internal set; } = null!;

        public Message Message { get; } = null!;

        public string? HeaderFileName { get; internal set; }
        public string? Body { get; internal set; }
        public string? Footer { get; internal set; }
    }
}
