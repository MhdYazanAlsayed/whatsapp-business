using SprintBusiness.Domain.Flows.FlowMessageButtons.Keys;
using SprintBusiness.Domain.Flows.FlowMessages.Keys;

namespace SprintBuisness.Contracts.Whatsapp.Dtos.MessageResponse
{
    public class SignalRFlowMessageButtonResponse
    {
        public required FlowMessageButtonId Id { get; set; }
        public required string DisplayText { get; set; }
        public required FlowMessageId Next { get; set; }
    }
}
