using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Messages.Enums;

namespace SprintBuisness.Contracts.Whatsapp.Dtos.MessageResponse
{
    public class SignalRMessageResponse
    {
        public string? Content { get; set; }
        public required ConversationId ConversationId { get; set; }
        public required bool Received { get; set; }
        public required MessageType Type { get; set; }
        public required bool IsNotify { get; set; }
        public required DateTime CreatedAt { get; set; }
        public SignalRFlowMessageResponse? FlowMessage { get; set; }
    }
}
