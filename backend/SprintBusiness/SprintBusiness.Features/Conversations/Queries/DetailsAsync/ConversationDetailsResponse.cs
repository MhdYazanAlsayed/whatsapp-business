using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Messages;

namespace SprintBusiness.Features.Conversations.Queries.DetailsAsync
{
    public class ConversationDetailsResponse
    {
        public Conversation? Conversation { get; set; }
        public int MessagePages { get; set; }
        public List<Message>? Messages { get; set; }
    }
}
