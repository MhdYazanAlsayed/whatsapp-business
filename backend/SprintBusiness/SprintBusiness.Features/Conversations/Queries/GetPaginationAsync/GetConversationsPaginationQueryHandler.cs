using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.Contracts.Authentication;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Extensions;

namespace SprintBusiness.Features.Conversations.Queries.GetPaginationAsync
{
    public class GetConversationsPaginationQueryHandler : IRequestHandler<GetConversationsPaginationQuery, PaginationDto<Conversation>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorization _authorization;

        public GetConversationsPaginationQueryHandler(ApplicationDbContext context ,
            IAuthorization authorization)
        {
            _context = context;
            _authorization = authorization;
        }

        public async Task<PaginationDto<Conversation>> Handle(GetConversationsPaginationQuery request, CancellationToken cancellationToken)
        {
            var employeeId = _authorization.GetCurrentEmployeeId();
            if (employeeId is null)
                throw new NullReferenceException();

            var data = await _context.Conversations
                .Include(nameof(Conversation.Contact))
                .Where(x => x.Owner == request.Type)
                .Where(x => request.Type != ConversationOwner.User || 
                    x.CustomerServiceEmployeeId == employeeId)
                .OrderByDescending(x => x.UpdatedAt)
                .ToPaginationAsync(request.Page);

            return data;
        }
    }
}
