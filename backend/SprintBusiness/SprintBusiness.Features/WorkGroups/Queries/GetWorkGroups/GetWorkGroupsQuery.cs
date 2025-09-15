using MediatR;
using SprintBusiness.Domain.Users.WorkGroups;

namespace SprintBusiness.Features.WorkGroups.Queries.GetWorkGroups
{
    public class GetWorkGroupsQuery(string? keyword = null) : IRequest<List<WorkGroup>>
    {
        public string? Keyword { get; set; } = keyword;
    }
}
