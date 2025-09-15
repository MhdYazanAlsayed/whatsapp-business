using MediatR;
using SprintBusiness.Domain.Templates.Keys;

namespace SprintBusiness.Domain.Messages.Events
{
    public class SendTemplateMessageEvent : INotification
    {
        public string PhoneNumber { get; set; } = null!;
        public TemplateId TemplateId { get; set; } = null!;
    }
}
