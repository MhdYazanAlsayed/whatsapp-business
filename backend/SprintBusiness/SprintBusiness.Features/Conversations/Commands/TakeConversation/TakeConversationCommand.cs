using MediatR;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.Conversations.Commands.TakeConversation
{
    public class TakeConversationCommand(int id) : IRequest<ResultDto>
    {
        public int ConversationId { get; set; } = id;
    }
}
