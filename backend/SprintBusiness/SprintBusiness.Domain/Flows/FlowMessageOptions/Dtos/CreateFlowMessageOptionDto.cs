using SprintBusiness.Domain.Flows.FlowMessages.Keys;

namespace SprintBusiness.Domain.Flows.FlowMessageItems.Dtos
{
    public class CreateFlowMessageOptionDto
    {
        public required string Content { get; set; }
        public required int Number { get; set; }
        public required FlowMessageId Next { get; set; }
        public FlowMessageId? MessageId { get; set; }
    }
}
