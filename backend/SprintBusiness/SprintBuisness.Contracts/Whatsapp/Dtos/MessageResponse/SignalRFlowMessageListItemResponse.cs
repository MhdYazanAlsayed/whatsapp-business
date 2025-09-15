using SprintBusiness.Domain.Flows.FlowMessageLists.Keys;
using SprintBusiness.Domain.Flows.FlowMessages.Keys;

namespace SprintBuisness.Contracts.Whatsapp.Dtos.MessageResponse
{
    public class SignalRFlowMessageListItemResponse
    {
        public required FlowMessageListItemId Id { get; set; }
        public required string Content { get; set; }
        public string? Description { get; set; }
        public required FlowMessageId Next { get; set; }
    }
}
