using MediatR;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.ReplyTemplates;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Extensions;

namespace SprintBusiness.Features.ReplyTemplates.Queries.GetPagination
{
    public class GetReplyTemplatesPaginationQueryHandler : IRequestHandler<GetReplyTemplatesPaginationQuery, PaginationDto<ReplyTemplate>>
    {
        private readonly ApplicationDbContext _context;

        public GetReplyTemplatesPaginationQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginationDto<ReplyTemplate>> Handle(GetReplyTemplatesPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.ReplyTemplates.ToPaginationAsync(request.Page);
        }
    }
}
