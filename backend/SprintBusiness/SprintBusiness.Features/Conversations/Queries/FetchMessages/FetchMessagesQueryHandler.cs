using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Messages;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Extensions;

namespace SprintBusiness.Features.Conversations.Queries.FetchMessages
{
    public class FetchMessagesQueryHandler : IRequestHandler<FetchMessagesQuery, PaginationDto<Message>>
    {
        private readonly ApplicationDbContext _context;

        public FetchMessagesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginationDto<Message>> Handle(FetchMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _context.Messages
                .OrderByDescending(x => x.CreatedAt)
                .Where(x => x.ConversationId == new ConversationId(request.ConversationId))
                .Include(x => x.FlowMessage)
                .Include(x => x.FlowMessage!.Buttons)
                .Include(x => x.FlowMessage!.ListItems)
                .Include(x => x.Sender)
                .Include(x => x.History)
                .Include(x => x.History!.Employee)
                .ToPaginationAsync(request.Page);

            return messages;
        }
    }
}
