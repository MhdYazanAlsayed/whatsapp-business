using MediatR;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Templates;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Extensions;

namespace SprintBusiness.Features.Templates.Queries.GetPagination
{
    public class GetTemplatesPaginationHandler : IRequestHandler<GetTemplatesPaginationQuery, PaginationDto<Template>>
    {
        private readonly ApplicationDbContext _context;

        public GetTemplatesPaginationHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginationDto<Template>> Handle(GetTemplatesPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Templates.ToPaginationAsync(request.Page);
        }
    }
} 