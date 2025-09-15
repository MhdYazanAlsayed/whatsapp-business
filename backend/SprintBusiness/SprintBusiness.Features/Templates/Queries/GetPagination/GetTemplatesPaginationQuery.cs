using MediatR;
using SprintBusiness.Domain.Templates;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.Templates.Queries.GetPagination
{
    public class GetTemplatesPaginationQuery : IRequest<PaginationDto<Template>>
    {
        public GetTemplatesPaginationQuery(int page)
        {
            Page = page;
        }
        public int Page { get; set; }
    }
} 