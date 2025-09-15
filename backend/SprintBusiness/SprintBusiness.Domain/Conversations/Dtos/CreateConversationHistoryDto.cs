using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Domain.Users.Keys;

namespace SprintBusiness.Domain.Conversations.Dtos
{
    public class CreateConversationHistoryDto
    {
        public required ConversationId ConversationId { get; set; }
        public required ConversationOwner CurrentOwner { get; set; }
        public int? EmployeeId { get; set; }
        public WorkGroupId? WorkGroupId { get; set; }
    }
}
