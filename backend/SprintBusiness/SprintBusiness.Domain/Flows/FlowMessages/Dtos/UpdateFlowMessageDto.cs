using SprintBusiness.Domain.Flows.Keys;

namespace SprintBusiness.Domain.Flows.FlowMessages.Dtos
{
    public class UpdateFlowMessageDto
    {
        public required string Content { get; set; }
        public FlowId? FlowId { get; set; }
    }
}
