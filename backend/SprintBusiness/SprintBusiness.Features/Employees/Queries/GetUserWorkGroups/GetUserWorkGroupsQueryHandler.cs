using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.Contracts.Authentication;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Users.WorkGroups;

namespace SprintBusiness.Features.Users.Queries.GetUserWorkGroups
{
    public class GetUserWorkGroupsQueryHandler : IRequestHandler<GetUserWorkGroupsQuery, List<WorkGroup>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorization _authorization;

        public GetUserWorkGroupsQueryHandler(ApplicationDbContext context , 
            IAuthorization authorization)
        {
            _context = context;
            _authorization = authorization;
        }

        public async Task<List<WorkGroup>> Handle(GetUserWorkGroupsQuery request, CancellationToken cancellationToken)
        {
            var employeeId = _authorization.GetCurrentEmployeeId();

            var result = await _context.Employees
                .Include(x => x.WorkGroups)
                .SingleAsync(x => x.Id == employeeId);

            return result.WorkGroups;
        }
    }
}
