using SprintBusiness.Domain.Flows.FlowMessages.Keys;

namespace SprintBusiness.Domain.Conversations.Dtos
{
    public class SendConversationMessageDto
    {
        public required bool FromBot { get; set; }
        public string? Content { get; set; }
        public FlowMessageId? MessageId { get; set; }
        public string? UserId { get; set; }
    }
}
