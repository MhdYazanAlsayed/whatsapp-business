using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Templates;

namespace SprintBusiness.Features.Templates.Queries.Details
{
    public class GetTemplateDetailsQueryHandler : IRequestHandler<GetTemplateDetailsQuery, Template?>
    {
        private readonly ApplicationDbContext _context;

        public GetTemplateDetailsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Template?> Handle(GetTemplateDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Templates
                .Include(x => x.Components)
                .ThenInclude(x => x.Variables)
                .Include(x => x.Buttons)
                .SingleOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}
