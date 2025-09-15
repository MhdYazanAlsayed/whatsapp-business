using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Flows.FlowMessages.Keys;
using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Domain.Messages.Templates;

namespace SprintBusiness.Domain.Messages.Dtos
{
    public class CreateMessageDto
    {
        public ConversationId? ConversationId { get; set; }
        public FlowMessageId? FlowMessageId { get; set; }
        public TemplateMessage? TemplateMessage { get; set; }
        public string? Content { get; set; }
        public required bool Received { get; set; }
        public required MessageType Type { get; set; }
        public required bool FromBot { get; set; }
        public int? SenderId { get; set; }
    }
}
