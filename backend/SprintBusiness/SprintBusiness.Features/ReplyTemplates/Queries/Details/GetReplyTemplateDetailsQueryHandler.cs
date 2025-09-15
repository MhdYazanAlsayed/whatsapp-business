using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.ReplyTemplates;

namespace SprintBusiness.Features.ReplyTemplates.Queries.Details
{
    public class GetReplyTemplateDetailsQueryHandler : IRequestHandler<GetReplyTemplateDetailsQuery, ReplyTemplate?>
    {
        private readonly ApplicationDbContext _context;

        public GetReplyTemplateDetailsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReplyTemplate?> Handle(GetReplyTemplateDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _context.ReplyTemplates.SingleOrDefaultAsync(x  => x.Id == request.Id);    
        }
    }
}
