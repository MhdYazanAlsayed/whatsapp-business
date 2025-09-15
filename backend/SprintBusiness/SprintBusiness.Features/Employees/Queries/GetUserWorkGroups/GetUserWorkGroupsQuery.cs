using MediatR;
using SprintBusiness.Domain.Users.WorkGroups;

namespace SprintBusiness.Features.Users.Queries.GetUserWorkGroups
{
    public class GetUserWorkGroupsQuery : IRequest<List<WorkGroup>>
    {
    }
}
