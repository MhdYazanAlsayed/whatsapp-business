using MediatR;
using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Domain.Users.Keys;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.Conversations.Commands.ConvertConversation
{
    public class ConvertConversationCommand(int id , ConversationOwner to , int? employeeId = null , int? workGroupId = null) : IRequest<ResultDto>
    {
        public ConversationId ConversationId { get; set; } = new ConversationId(id);
        public ConversationOwner To { get; set; } = to;
        public int? EmployeeId { get; set; } = employeeId;
        public WorkGroupId? WorkGroupId { get; set; } = workGroupId != null ? new WorkGroupId((int)workGroupId) : null;
    }
}
