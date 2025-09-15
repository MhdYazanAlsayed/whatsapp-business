using MediatR;
using SprintBusiness.Domain.Conversations.Keys;

namespace SprintBusiness.Features.Conversations.Queries.GetConvertOptions
{
    public class GetConvertOptionsQuery(int? id) : IRequest<List<ConvertOptionsResultDto>>
    {
        public ConversationId? Id { get; set; } = id != null ? new((int)id) : null;
    }
}
