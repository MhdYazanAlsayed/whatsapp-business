using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SprintBuisness.Contracts.Authentication;
using SprintBuisness.EntityframeworkCore.Contexts;

namespace SprintBusiness.Features.Conversations.Commands.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand>
    {
        private readonly IAuthorization _authorization;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SendMessageCommandHandler> _logger;

        public SendMessageCommandHandler(IAuthorization authorization ,
            ApplicationDbContext context ,
            ILogger<SendMessageCommandHandler> logger)
        {
            _authorization = authorization;
            _context = context;
            _logger = logger;
        }
        public async Task Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("SendMessageCommandHandler");
            var employeeId = _authorization.GetCurrentEmployeeId();
            if (employeeId is null)
                throw new UnauthorizedAccessException();

            _logger.LogInformation("Find the user. {employeeId}", employeeId);

            var user = await _context.Employees
                .Include(x => x.Conversations.Where(a => a.Id == request.ConversationId))
                .ThenInclude(x => x.Contact)
                .SingleAsync(x => x.Id == employeeId);

            _logger.LogInformation("Send the message. {content}", request.Content);
            user.SendMessage(request.ConversationId, request.Content);

            _context.Update(user);
            
            _logger.LogInformation("Save the message. {content}", request.Content);
            await _context.SaveChangesAsync();
        }
    }
}
