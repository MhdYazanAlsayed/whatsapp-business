using SprintBuisness.Contracts.Whatsapp;
using SprintBuisness.Contracts.Whatsapp.Dtos;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Flows.FlowMessages.Keys;

namespace SprintBuisness.Application.Bot.FlowProcessors
{
    public abstract class BaseFlowProcessor
    {
        private readonly ApplicationDbContext _context;
        protected readonly Conversation _conversation;
        protected readonly IRealtime _realtime;

        protected BaseFlowProcessor(ApplicationDbContext context , Conversation conversation ,
            IRealtime realtime)
        {
            _context = context;
            _conversation = conversation;
            _realtime = realtime;
        }

        protected async Task SaveAsync (FlowMessageId messageId , string? content = null)
        {
            _conversation.AddFlowMessage(messageId , content);

            _context.Conversations.Update(_conversation);
            await _context.SaveChangesAsync();
        }

        protected async Task SendMessageRealtimeAsync (RealtimeMessageDto dto)
        {
            await _realtime.SendMessageAsync(dto);
        }
    }
}
