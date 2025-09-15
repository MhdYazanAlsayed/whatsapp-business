using MediatR;

namespace SprintBusiness.Domain.Messages.DomainEvents
{
    public class SendTextMessageEvent : INotification
    {
        public required string CustomerServiceName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Content { get; set; }
    }
}
