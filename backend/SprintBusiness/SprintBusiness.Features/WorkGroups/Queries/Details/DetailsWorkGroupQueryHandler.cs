using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Users.WorkGroups;

namespace SprintBusiness.Features.WorkGroups.Queries.Details
{
    public class DetailsWorkGroupQueryHandler : IRequestHandler<DetailsWorkGroupQuery, WorkGroup?>
    {
        private readonly ApplicationDbContext _context;

        public DetailsWorkGroupQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WorkGroup?> Handle(DetailsWorkGroupQuery request, CancellationToken cancellationToken)
        {
            return await _context.WorkGroups
                .Include(x => x.Employees)
                .SingleOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}
