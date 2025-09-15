using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.Contracts.Authentication;
using SprintBuisness.Contracts.Whatsapp;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Base;
using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Helpers;

namespace SprintBusiness.Features.Conversations.Commands.ConvertConversation
{
    public class ConvertConversationCommandHandler : IRequestHandler<ConvertConversationCommand, ResultDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IRealtime _realtime;
        private readonly IAuthorization _authorization;
        private readonly IWhatsappBot _whatsappBot;

        public ConvertConversationCommandHandler(ApplicationDbContext context ,
            IRealtime realtime ,
            IAuthorization authorization , 
            IWhatsappBot whatsappBot)
        {
            _context = context;
            _realtime = realtime;
            _authorization = authorization;
            _whatsappBot = whatsappBot;
        }

        public async Task<ResultDto> Handle(ConvertConversationCommand request, CancellationToken cancellationToken)
        {
            var userId = _authorization.GetLoggedUserId();
            if (userId is null) 
                return ResultDto.Failure();

            var conversation = await _context.Conversations
                .SingleAsync(x => x.Id == request.ConversationId);

            try
            {
                await ConvertAsync(conversation, request);

                //await _realtime.UpdateConversationAsync(new()
                //{
                    
                //});

                // If the conversation will return to bot u can send evaluation message
                // Thats why i injected the whatsapp bot service

                return ResultDto.Success();
            }
            catch(Exception ex)
            {
                return ResultDto.Failure(ex.Message);
            }
        }

        private async Task ConvertAsync (Conversation conversation , ConvertConversationCommand request)
        {
            var employeeId = _authorization.GetCurrentEmployeeId();
            if (employeeId is null)
                throw new Exception("Employee not found");

            conversation.Convert(new()
            {
                To = request.To,
                RecipientId = request.EmployeeId,
                WorkGroupId = request.WorkGroupId ,
                EmployeeId = employeeId
            });

            _context.Update(conversation);
            await _context.SaveChangesAsync();

        }
    }
}
