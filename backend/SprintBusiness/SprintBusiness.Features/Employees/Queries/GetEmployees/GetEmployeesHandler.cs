using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Users;

namespace SprintBusiness.Features.Users.Queries.GetUsers
{
    public class GetEmployeesHandler : IRequestHandler<GetEmployeesQuery, List<Employee>>
    {
        private readonly ApplicationDbContext _context;

        public GetEmployeesHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .Where(x => request.Keyword == null || x.EnglishName.Contains(request.Keyword) || x.ArabicName.Contains(request.Keyword))
                .OrderBy(x => x.EnglishName)
                .ToListAsync();
        }
    }
}
