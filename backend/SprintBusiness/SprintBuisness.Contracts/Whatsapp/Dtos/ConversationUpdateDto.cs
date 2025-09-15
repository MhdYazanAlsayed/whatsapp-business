using SprintBusiness.Domain.Conversations;

namespace SprintBuisness.Contracts.Whatsapp.Dtos
{
    public class ConversationUpdateDto
    {
        public required bool Add { get; set; }
        public required Conversation Conversation { get; set; }
    }
}
