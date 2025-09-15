using SprintBuisness.Contracts.Whatsapp;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBuisness.Whatapp;
using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Flows.FlowMessages;
using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Shared.Helpers;

namespace SprintBuisness.Application.Bot.FlowProcessors
{
    public class NoneFlowProcessor : BaseFlowProcessor, IFlowProesessor
    {
        private readonly IWhatsappProvider _whatsappProvider;

        public NoneFlowProcessor(
            IWhatsappProvider whatsappProvider , 
            IRealtime realTime,
            ApplicationDbContext context,
            Conversation conversation) : base(context, conversation , realTime)
        {
            _whatsappProvider = whatsappProvider;
        }

        public async Task SendAsync(FlowMessage message, string to, string? content = null)
        {
            var result = await _whatsappProvider.SendTextMessageAsync(new()
            {
                Message = content ?? message.Content,
                To = to
            });

            if (!result.Succeeded)
                throw new Exception("Couldn't send interactive message .");

            await SaveAsync(message.Id , content);
            await SendMessageRealtimeAsync(new()
            {
                ConversationId = _conversation.Id,
                CreatedAt = DateTimeCulture.Now,
                Content = content ?? message.Content,
                FlowMessage = message,
                IsNotify = false,
                Type = MessageType.FlowMessage,
                Received = false,
            });
        }
    }
}
