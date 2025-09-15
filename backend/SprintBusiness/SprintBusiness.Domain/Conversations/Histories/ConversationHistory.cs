using SprintBusiness.Domain.Base;
using SprintBusiness.Domain.Conversations.Dtos;
using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Domain.Users;
using SprintBusiness.Domain.Users.Keys;
using SprintBusiness.Domain.Users.WorkGroups;

namespace SprintBusiness.Domain.Conversations.Histories
{
    public class ConversationHistory : Entity
    {
        protected ConversationHistory() 
        {
            ConversationId = null!;
        }
        private ConversationHistory(CreateConversationHistoryDto dto)
        {
            ConversationId = dto.ConversationId;
            EmployeeId = dto.EmployeeId;
            CurrentOwner = dto.CurrentOwner;
            WorkGroupId = dto.WorkGroupId;
        }

        public ConversationHistoryId Id { get; private set; } = null!;
        public ConversationOwner CurrentOwner { get; private set; }

        public int? EmployeeId { get; private set; }
        public Employee? Employee { get; private set; }

        public ConversationId ConversationId { get; private set; }
        public Conversation? Conversation { get; private set; }

        public WorkGroupId? WorkGroupId { get; private set; }
        public WorkGroup? WorkGroup { get; private set; }

        internal static ConversationHistory Create(CreateConversationHistoryDto dto)
        {
            if (dto.CurrentOwner == ConversationOwner.User && dto.EmployeeId is null)
                throw new ArgumentNullException(nameof(dto.EmployeeId));

            if (dto.CurrentOwner == ConversationOwner.WorkGroup && dto.WorkGroupId is null)
                throw new ArgumentNullException(nameof(dto.WorkGroupId));

            return new ConversationHistory(dto);
        }
    }
}
