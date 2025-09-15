using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Users.WorkGroups;

namespace SprintBusiness.Features.WorkGroups.Queries.GetWorkGroups
{
    public class GetWorkGroupsQueryHandler : IRequestHandler<GetWorkGroupsQuery, List<WorkGroup>>
    {
        private readonly ApplicationDbContext _context;

        public GetWorkGroupsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<WorkGroup>> Handle(GetWorkGroupsQuery request, CancellationToken cancellationToken)
        {
            return await _context.WorkGroups
                .Where(x => request.Keyword == null || x.Name.Contains(request.Keyword))
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
    }
}
