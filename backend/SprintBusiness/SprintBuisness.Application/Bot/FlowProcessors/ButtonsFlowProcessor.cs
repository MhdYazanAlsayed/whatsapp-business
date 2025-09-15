using SprintBuisness.Contracts.Whatsapp;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBuisness.Whatapp;
using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Flows.FlowMessages;
using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Shared.Helpers;
using SprintBusiness.Whatsapp.Dtos;

namespace SprintBuisness.Application.Bot.FlowProcessors
{
    public class ButtonsFlowProcessor : BaseFlowProcessor , IFlowProesessor
    {
        private readonly IWhatsappProvider _whatsappProvider;

        public ButtonsFlowProcessor(
            IWhatsappProvider whatsappProvider, 
            IRealtime realtime ,
            ApplicationDbContext context,
            Conversation conversation) : base(context, conversation , realtime)
        {
            _whatsappProvider = whatsappProvider;
        }

        public async Task SendAsync(FlowMessage flowMessage, string to, string? content = null)
        {
            var result = await _whatsappProvider.SendReplayButtonMessageAsync(new()
            {
                Message = content ?? flowMessage.Content,
                To = to,
                Buttons = flowMessage.Buttons.Select(x => new InteractiveMessageButton
                {
                    Id = x.Id.Value.ToString(),
                    Text = x.DisplayText
                }).ToList(),
            });

            if (!result.Succeeded)
                throw new Exception("Couldn't send interactive message .");

            await SaveAsync(flowMessage.Id , content);
            await SendMessageRealtimeAsync(new() 
            {
                ConversationId = _conversation.Id,
                CreatedAt = DateTimeCulture.Now ,
                Content = content ?? flowMessage.Content,
                FlowMessage = flowMessage , 
                IsNotify = false ,
                Type = MessageType.FlowMessage ,
                Received = false,
            });
        }
    }
}
