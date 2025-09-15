using MediatR;
using SprintBusiness.Domain.Users;
using SprintBusiness.Domain.Users.Keys;

namespace SprintBusiness.Features.WorkGroups.Queries.GetMembers
{
    public class GetWorkGroupMembersQuery(int id) : IRequest<List<Employee>>
    {
        public WorkGroupId Id { get; set; } = new(id);
    }
}
