using MediatR;
using SprintBuisness.Contracts.Authentication;
using SprintBuisness.Contracts.Database.UnitOfWorks;
using SprintBuisness.Contracts.Whatsapp;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Exceptions.Conversations;

namespace SprintBusiness.Features.Conversations.Commands.TakeConversation
{
    public class TakeConversationCommandHandler : IRequestHandler<TakeConversationCommand, ResultDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorization _authorization;
        private readonly IRealtime _realTime;
        private readonly ApplicationDbContext _context;

        public TakeConversationCommandHandler(IUnitOfWork unitOfWork , 
            IAuthorization authorization ,
            IRealtime realTime,
            ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _authorization = authorization;
            _realTime = realTime;
            _context = context;
        }

        public async Task<ResultDto> Handle(TakeConversationCommand request, CancellationToken cancellationToken)
        {
            var user = await _authorization.GetCurrentUser();
            if (user is null)
                throw new NullReferenceException();

            try
            {
                var conversation = await _unitOfWork.Conversations
                    .SingleAsync(x => x.Id == new ConversationId(request.ConversationId));

                user.TakeConversation(conversation);

                _context.Update(user);
                await _context.SaveChangesAsync();

                await _realTime.UpdateConversationAsync(new() 
                {
                    Conversation = conversation,
                    Add = false
                });

                return ResultDto.Success();
            }
            catch (ConversationAlreadyTakenException)
            {
                return ResultDto.Failure("Cannot take conversation is already taken !");
            }
            catch (Exception ex)
            {
                return ResultDto.Failure(ex.Message);
            }
        }
    }
}
