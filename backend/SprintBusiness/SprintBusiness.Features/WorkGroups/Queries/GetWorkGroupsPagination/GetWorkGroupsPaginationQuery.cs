using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SprintBusiness.Domain.Users.WorkGroups;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.WorkGroups.Queries.GetWorkGroupsPagination
{
    public class GetWorkGroupsPaginationQuery(int page, string? keyword = null) : IRequest<PaginationDto<WorkGroup>>
    {
        [BindRequired]
        public int Page { get; set; } = page;

        public string? Keyword { get; set; } = keyword;
    }
}
