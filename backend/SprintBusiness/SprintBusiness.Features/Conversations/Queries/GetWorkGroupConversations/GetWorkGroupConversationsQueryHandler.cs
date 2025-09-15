using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Conversations;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Extensions;

namespace SprintBusiness.Features.Conversations.Queries.GetWorkGroupConversations
{
    public class GetWorkGroupConversationsQueryHandler : IRequestHandler<GetWorkGroupConversationsQuery, PaginationDto<Conversation>>
    {
        private readonly ApplicationDbContext _context;

        public GetWorkGroupConversationsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginationDto<Conversation>> Handle(GetWorkGroupConversationsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Conversations
                .Where(x => x.WorkGroupId == request.WorkGroupId)
                .Include(nameof(Conversation.Contact))
                .OrderByDescending(x => x.UpdatedAt)
                .ToPaginationAsync(request.Page);
        }
    }
}
