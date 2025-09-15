using MediatR;
using SprintBusiness.Domain.Conversations.Keys;

namespace SprintBusiness.Features.Conversations.Commands.SendMessage
{
    public class SendMessageCommand(int id , string content) : IRequest
    {
        public ConversationId ConversationId { get; set; } = new(id);
        public string Content { get; set; } = content;
    }
}
