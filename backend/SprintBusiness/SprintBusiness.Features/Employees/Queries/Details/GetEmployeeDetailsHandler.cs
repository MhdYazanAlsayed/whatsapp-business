using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Users;

namespace SprintBusiness.Features.Users.Queries.Details
{
    public class GetEmployeeDetailsHandler : IRequestHandler<GetEmployeeDetailsQuery, Employee?>
    {
        private readonly ApplicationDbContext _context;

        public GetEmployeeDetailsHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee?> Handle(GetEmployeeDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .Include(x => x.WorkGroups)
                .SingleOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}
