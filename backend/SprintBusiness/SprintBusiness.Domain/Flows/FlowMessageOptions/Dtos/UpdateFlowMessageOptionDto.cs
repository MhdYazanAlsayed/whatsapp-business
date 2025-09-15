using SprintBusiness.Domain.Flows.FlowMessages.Keys;

namespace SprintBusiness.Domain.Flows.FlowMessageItems.Dtos
{
    public class UpdateFlowMessageOptionDto
    {
        public required string Content { get; set; }
        public required FlowMessageId Next { get; set; }
    }
}
