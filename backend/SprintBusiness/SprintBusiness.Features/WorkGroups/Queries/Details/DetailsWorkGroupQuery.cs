using MediatR;
using SprintBusiness.Domain.Users.Keys;
using SprintBusiness.Domain.Users.WorkGroups;

namespace SprintBusiness.Features.WorkGroups.Queries.Details
{
    public class DetailsWorkGroupQuery(int id) : IRequest<WorkGroup?>
    {
        public WorkGroupId Id { get; set; } = new(id);
    }
}
