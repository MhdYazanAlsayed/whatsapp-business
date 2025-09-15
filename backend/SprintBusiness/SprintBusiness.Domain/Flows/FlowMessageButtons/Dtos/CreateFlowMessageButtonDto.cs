using SprintBusiness.Domain.Flows.FlowMessages.Keys;

namespace SprintBusiness.Domain.Flows.FlowMessageButtons.Dtos
{
    public class CreateFlowMessageButtonDto
    {
        public required string DisplayText { get; set; }
        public required FlowMessageId Next { get; set; }
        public FlowMessageId? MessageId { get; set; }
    }
}
