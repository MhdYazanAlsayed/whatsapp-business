using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Flows.FlowMessages;
using SprintBusiness.Domain.Messages.Enums;

namespace SprintBuisness.Contracts.Whatsapp.Dtos
{
    public class RealtimeMessageDto
    {
        public string? Content { get; set; }
        public required bool Received { get; set; }
        public required ConversationId ConversationId { get; set; }
        //public required Conversation Conversation { get; set; }
        public required MessageType Type { get; set; }
        public required bool IsNotify { get; set; }
        //public FlowMessageId? FlowMessageId { get; set; }
        public FlowMessage? FlowMessage { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}
