using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Messages.Enums;

namespace SprintBuisness.Contracts.Whatsapp.Dtos
{
    public class SignalRConversationResponse
    {
        public required ConversationId Id { get; set; }
        public required SignalRContactResposne Contact { get; set; }
        public ConversationOwner Owner { get; set; }
        public string? CustomerServiceUserId { get; set; }
        public bool FromBot { get; set; }

    }
}
