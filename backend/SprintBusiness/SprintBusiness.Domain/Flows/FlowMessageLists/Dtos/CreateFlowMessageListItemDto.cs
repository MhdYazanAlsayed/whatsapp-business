using SprintBusiness.Domain.Flows.FlowMessages.Keys;

namespace SprintBusiness.Domain.Flows.FlowMessageLists.Dtos
{
    public class CreateFlowMessageListItemDto
    {
        public required string Content { get; set; }
        public FlowMessageId? MessageId { get; set; }
        public required FlowMessageId Next { get; set; }
        public string? Description { get; set; }
    }
}
