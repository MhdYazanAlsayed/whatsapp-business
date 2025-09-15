using MediatR;
using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.Conversations.Queries.GetPaginationAsync
{
    public class GetConversationsPaginationQuery(ConversationOwner owner , int page) : IRequest<PaginationDto<Conversation>>
    {
        public ConversationOwner Type { get; set; } = owner;
        public int Page { get; set; } = page;
    }
}
