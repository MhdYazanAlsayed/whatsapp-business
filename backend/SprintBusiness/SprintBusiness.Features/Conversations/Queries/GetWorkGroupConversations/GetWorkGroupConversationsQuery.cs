using MediatR;
using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Users.Keys;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.Conversations.Queries.GetWorkGroupConversations
{
    public class GetWorkGroupConversationsQuery(int workGroupId , int page) : IRequest<PaginationDto<Conversation>>
    {
        public WorkGroupId WorkGroupId { get; set; } = new WorkGroupId(workGroupId);
        public int Page { get; set; } = page;
    }
}
