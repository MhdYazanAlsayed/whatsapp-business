using MediatR;
using SprintBuisness.Whatapp;
using SprintBusiness.Domain.Messages.DomainEvents;

namespace SprintBuisness.DomainEvents.Messages.SendTextMessage
{
    public class SendTextMessageDomainEvent : INotificationHandler<SendTextMessageEvent>
    {
        private readonly IWhatsappProvider _whatsappProvider;

        public SendTextMessageDomainEvent(IWhatsappProvider whatsappProvider)
        {
            _whatsappProvider = whatsappProvider;
        }

        public async Task Handle(SendTextMessageEvent notification, CancellationToken cancellationToken)
        {
            var result = await _whatsappProvider.SendTextMessageAsync(new()
            {
                To = notification.PhoneNumber,
                Message = $"موظف خدمة العملاء : {notification.CustomerServiceName}\n" + notification.Content
            });

            if (!result.Succeeded)
                throw new InvalidOperationException("Failed to send text message");
        }
    }
}
