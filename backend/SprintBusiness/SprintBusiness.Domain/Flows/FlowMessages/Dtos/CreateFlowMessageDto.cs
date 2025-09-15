using SprintBusiness.Domain.Flows.FlowMessages.Enums;
using SprintBusiness.Domain.Flows.Keys;

namespace SprintBusiness.Domain.Flows.FlowMessages.Dtos
{
    public class CreateFlowMessageDto
    {
        public required string Content { get; set; }
        public required FlowMessageAction Action { get; set; }
        public required FlowMessageType Type { get; set; }
        public required FlowMessageEventType EventType { get; set; }
        public required int Number { get; set; }
        public FlowId? FlowId { get; set; }
    }
}
