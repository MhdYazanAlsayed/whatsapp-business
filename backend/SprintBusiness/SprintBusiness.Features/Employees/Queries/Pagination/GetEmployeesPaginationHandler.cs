using MediatR;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Users;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Extensions;

namespace SprintBusiness.Features.Users.Queries.Pagination
{
    public class GetEmployeesPaginationHandler : IRequestHandler
        <GetEmployeesPaginationQuery, PaginationDto<Employee>>
    {
        private readonly ApplicationDbContext _context;

        public GetEmployeesPaginationHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginationDto<Employee>> Handle(GetEmployeesPaginationQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Employees
                .Where(x => request.Keyword == null || x.EnglishName.Contains(request.Keyword) || x.ArabicName.Contains(request.Keyword))
                .OrderBy(x => x.EnglishName)
                .ToPaginationAsync(request.Page);

            return users;
        }
    }
}
