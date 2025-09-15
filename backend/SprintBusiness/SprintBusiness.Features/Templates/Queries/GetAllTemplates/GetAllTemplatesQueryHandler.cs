using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Templates;

namespace SprintBusiness.Features.Templates.Queries.GetAllTemplates
{
    public class GetAllTemplatesQueryHandler : IRequestHandler<GetAllTemplatesQuery, List<Template>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllTemplatesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Template>> Handle(GetAllTemplatesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Templates.ToListAsync();
        }
    }
}
