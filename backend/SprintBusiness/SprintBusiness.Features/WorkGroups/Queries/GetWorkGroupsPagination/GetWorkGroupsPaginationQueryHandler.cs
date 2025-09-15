using MediatR;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Users.WorkGroups;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Extensions;

namespace SprintBusiness.Features.WorkGroups.Queries.GetWorkGroupsPagination
{
    public class GetWorkGroupsPaginationQueryHandler : IRequestHandler<GetWorkGroupsPaginationQuery, PaginationDto<WorkGroup>>
    {
        private readonly ApplicationDbContext _context;

        public GetWorkGroupsPaginationQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginationDto<WorkGroup>> Handle(GetWorkGroupsPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.WorkGroups
                .Where(x => request.Keyword == null || x.Name.Contains(request.Keyword))
                .OrderBy(x => x.Name)
                .ToPaginationAsync(request.Page);
        }
    }
}
