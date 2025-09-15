using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Domain.Users.Keys;

namespace SprintBusiness.Domain.Users.Dtos
{
    public class ConvertUserConversationDto
    {
        public required ConversationId ConversationId { get; set; }
        public required ConversationOwner To { get; set; }
        public int? RecipientId { get; set; }
        public WorkGroupId? WorkGroupId { get; set; }
    }
}
