using MediatR;
using SprintBusiness.Domain.Messages;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.Conversations.Queries.FetchMessages
{
    public class FetchMessagesQuery(int id , int page) : IRequest<PaginationDto<Message>>
    {
        public int ConversationId { get; set; } = id;
        public int Page { get; set; } = page;
    }
}
