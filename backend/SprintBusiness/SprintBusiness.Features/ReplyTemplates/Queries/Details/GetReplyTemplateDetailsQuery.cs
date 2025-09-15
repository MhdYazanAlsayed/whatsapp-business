using MediatR;
using SprintBusiness.Domain.ReplyTemplates;
using SprintBusiness.Domain.ReplyTemplates.Keys;

namespace SprintBusiness.Features.ReplyTemplates.Queries.Details
{
    public class GetReplyTemplateDetailsQuery: IRequest<ReplyTemplate?>
    {
        public GetReplyTemplateDetailsQuery(int id)
        {
            Id = new(id);
        }

        public ReplyTemplateId Id { get; set; }
    }
}
