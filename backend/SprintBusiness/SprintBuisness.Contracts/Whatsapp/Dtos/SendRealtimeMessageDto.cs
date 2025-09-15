using SprintBusiness.Domain.Conversations.Keys;

namespace SprintBuisness.Contracts.Whatsapp.Dtos
{
    public class SendRealtimeMessageDto
    {
        public required ConversationId ConversationId { get; set; }
        public required RealtimeMessageDto Message { get; set; }
    }
}
