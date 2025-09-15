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
    public class ListFlowProcessor : BaseFlowProcessor , IFlowProesessor
    {
        private readonly IWhatsappProvider _whatsappProvider;

        public ListFlowProcessor(
            IWhatsappProvider whatsappProvider,
            IRealtime realtime,
            ApplicationDbContext context,
            Conversation conversation) : base(context, conversation , realtime)
        {
            _whatsappProvider = whatsappProvider;
        }

        public async Task SendAsync(FlowMessage message, string to , string? content = null)
        {
            var result = await _whatsappProvider.SendListMessageAsync(new()
            {
                Interactive = new()
                {
                    Body = new()
                    {
                        Text = content ?? message.Content,
                    },
                    Action = new()
                    {
                        Button = "اختيار",
                        Sections = new()
                        {
                            new ()
                            {
                                Title = "" ,
                                Rows = message.ListItems
                                .Select(x => new SendMenuMessageRowDto
                                {
                                    Id = x.Id.Value.ToString(),
                                    Title = x.Content
                                })
                                .ToList()
                            }
                        }
                    }
                },
                To = to
            });

            if (!result.Succeeded)
            {
                throw new Exception("Couldn't send interactive message .");
            }

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
