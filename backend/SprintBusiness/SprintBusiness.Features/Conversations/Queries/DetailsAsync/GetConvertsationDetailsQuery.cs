using MediatR;

namespace SprintBusiness.Features.Conversations.Queries.DetailsAsync
{
    public class GetConvertsationDetailsQuery(int id) : IRequest<ConversationDetailsResponse?>
    {
        public int Id { get; set; } = id;
    }
}
