using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Domain.Users.Keys;

namespace SprintBusiness.Domain.Conversations.Dtos
{
    public class ConvertConversationDto
    {
        public required ConversationOwner To { get; set; }
        public int? EmployeeId { get; set; }
        public int? RecipientId { get; set; }
        public WorkGroupId? WorkGroupId { get; set; }
    }
}
