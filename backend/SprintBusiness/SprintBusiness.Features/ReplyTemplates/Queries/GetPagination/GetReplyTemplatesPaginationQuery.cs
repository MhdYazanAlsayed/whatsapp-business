using MediatR;
using SprintBusiness.Domain.ReplyTemplates;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.ReplyTemplates.Queries.GetPagination
{
    public class GetReplyTemplatesPaginationQuery : IRequest<PaginationDto<ReplyTemplate>>
    {
        public GetReplyTemplatesPaginationQuery(int page)
        {
            Page = page;
        }
        public int Page { get; set; }
    }
}
