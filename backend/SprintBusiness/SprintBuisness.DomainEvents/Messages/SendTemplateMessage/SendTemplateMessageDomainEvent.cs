using MediatR;
using SprintBuisness.Whatapp;
using SprintBusiness.Domain.Messages.Events;
namespace SprintBuisness.DomainEvents.Messages.SendTemplateMessage
{
    public class SendTemplateMessageDomainEvent : INotificationHandler<SendTemplateMessageEvent>
    {
        private readonly IWhatsappProvider _whatsappProvider;

        public SendTemplateMessageDomainEvent(IWhatsappProvider whatsappProvider)
        {
            _whatsappProvider = whatsappProvider;
        }

        public async Task Handle(SendTemplateMessageEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
