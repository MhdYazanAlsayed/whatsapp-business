using SprintBusiness.Domain.Flows.FlowMessages.Enums;

namespace SprintBuisness.Contracts.Whatsapp.Dtos.MessageResponse
{
    public class SignalRFlowMessageResponse
    {
        public required string Content { get; set; }
        public required FlowMessageType Type { get; set; }
        public required FlowMessageAction Action { get; set; }
        public required FlowMessageEventType EventType { get; set; }
        public List<SignalRFlowMessageButtonResponse>? Buttons { get; set; }

        public string? ButtonListDisplayText { get; set; }
        public List<SignalRFlowMessageListItemResponse>? ListItems { get; set; }
    }
}
