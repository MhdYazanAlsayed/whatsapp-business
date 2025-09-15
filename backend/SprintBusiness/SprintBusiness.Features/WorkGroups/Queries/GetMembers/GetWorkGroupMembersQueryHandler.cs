using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Users;

namespace SprintBusiness.Features.WorkGroups.Queries.GetMembers
{
    public class GetWorkGroupMembersQueryHandler : IRequestHandler<GetWorkGroupMembersQuery, List<Employee>>
    {
        private readonly ApplicationDbContext _context;

        public GetWorkGroupMembersQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> Handle(GetWorkGroupMembersQuery request, CancellationToken cancellationToken)
        {
            var response = await _context.WorkGroups
                .Include(x => x.Employees)
                .SingleAsync(x => x.Id == request.Id);

            return response.Employees;
        }
    }
}
