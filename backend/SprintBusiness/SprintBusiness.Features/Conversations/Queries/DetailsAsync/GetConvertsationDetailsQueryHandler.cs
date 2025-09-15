using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.Contracts.Authentication;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Shared.Extensions;
using SprintBusiness.Shared.Helpers;

namespace SprintBusiness.Features.Conversations.Queries.DetailsAsync
{
    public class GetConvertsationDetailsQueryHandler : IRequestHandler<GetConvertsationDetailsQuery, ConversationDetailsResponse?>
    {
        private readonly IAuthorization _authorization;
        private readonly ApplicationDbContext _context;

        public GetConvertsationDetailsQueryHandler(IAuthorization authorization , ApplicationDbContext context)
        {
            _authorization = authorization;
            _context = context;
        }
        public async Task<ConversationDetailsResponse?> Handle(GetConvertsationDetailsQuery request, CancellationToken cancellationToken)
        {
            var employeeId = _authorization.GetCurrentEmployeeId();
            if (employeeId is null)
                throw new NullReferenceException();

            var conversation = await _context.Conversations
                .Include(x => x.Note)
                .ThenInclude(x => x!.Attachments)
                .Include(x => x.Contact)
                .Include(x => x.CustomerServiceEmployee)
                .SingleAsync(x => x.Id == new ConversationId(request.Id));

            if (conversation.Owner == ConversationOwner.User && conversation.CustomerServiceEmployeeId != employeeId)
                throw new InvalidOperationException();

            var result = await _context.Messages
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .AsSplitQuery()
                .Where(x => x.ConversationId == new ConversationId(request.Id))
                .Include(x => x.FlowMessage)
                .Include(x => x.FlowMessage!.Buttons)
                .Include(x => x.FlowMessage!.ListItems)
                .Include(x => x.TemplateMessage)
                .Include(x => x.History)
                .Include(x => x.History!.Employee)
                .Include(x => x.Sender)
                .ToPaginationAsync(1);

            // Add this user as a someone that opened this chat 
            // In order to send request to his browser to refresh the content .

            ConversationManager.AddUser(conversation.Id, employeeId.Value);

            return new ConversationDetailsResponse()
            {
                Conversation = conversation,
                MessagePages = result.Pages ,
                Messages = result.Data
            };
        }
    }
}
