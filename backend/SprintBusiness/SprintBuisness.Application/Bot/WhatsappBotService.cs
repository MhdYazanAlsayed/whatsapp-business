using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SprintBuisness.Application.Bot.FlowProcessors;
using SprintBuisness.Contracts.Whatsapp;
using SprintBuisness.Contracts.Whatsapp.Processors;
using SprintBuisness.Contracts.Whatsapp.Processors.Enums;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBuisness.Whatapp;
using SprintBusiness.Contracts.Whatsapp.Dtos;
using SprintBusiness.Domain.Contacts;
using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Flows.FlowMessageButtons.Keys;
using SprintBusiness.Domain.Flows.FlowMessageLists.Keys;
using SprintBusiness.Domain.Flows.FlowMessages;
using SprintBusiness.Domain.Flows.FlowMessages.Enums;
using SprintBusiness.Domain.Flows.FlowMessages.Keys;
using SprintBusiness.Shared.Enums;

namespace SprintBuisness.Application.Bot
{
    public class WhatsappBotService : IWhatsappBot
    {
        private readonly ILogger<WhatsappBotService> _logger;
        private readonly IWhatsappProvider _whatsappProvider;
        private readonly IRealtime _realtime;
        private readonly IMessageProcessorFactory _messageProcessorFactory;
        private readonly ApplicationDbContext _context;

        public WhatsappBotService(
            ILogger<WhatsappBotService> logger,
            IWhatsappProvider whatsappProvider,
            IRealtime realtime ,
            IMessageProcessorFactory messageProcessorFactory,
            ApplicationDbContext context)
        {
            _logger = logger;
            _whatsappProvider = whatsappProvider;
            _realtime = realtime;
            _messageProcessorFactory = messageProcessorFactory;
            _context = context;
        }

        public async Task GenarateAnAnswerAsync(GenarateAnswerDto dto)
        {
            if (dto.ContactInformation.IsNew)
            {
                await SendQuestionForCustomerNameAsync(dto);

                return;
            }

            await ProcessMessageAsync(dto);
        }

        public async Task SendEvaluationMessageAsync (Contact contact)
        {
            _logger.LogInformation("[Sending Evaluation Message ...]");

            var message = await _context.FlowMessages
                .Include(x => x.Buttons)
                .SingleOrDefaultAsync(x => x.EventType == FlowMessageEventType.Evaluation);

            if (message is null)
            {
                _logger.LogWarning("Cannot find evaluation message !");
                return;
            }

            await SendFlowMessageAsync(contact, message);
        }

        private async Task SendQuestionForCustomerNameAsync(GenarateAnswerDto dto)
        {
            // We have to use cache to store the question for customer name
            _logger.LogInformation("[Sending Question For CustomerName ...]");

            var question = await _context.FlowMessages
                .Include(nameof(FlowMessage.Buttons))
                .FirstAsync(x => x.EventType == FlowMessageEventType.AskForName);

            var content = question.Content
                .Replace("{FULLNAME}", dto.ContactInformation.FullName);

            var conversation = dto.ContactInformation.Contact.Conversation;

            await SendFlowMessageAsync(
                dto.ContactInformation.Contact ,
                question ,
                content
            );

            _logger.LogInformation("[Question For CustomerName Was Sent Successfully]");
        }

        private async Task ProcessMessageAsync(GenarateAnswerDto dto)
        {
            _logger.LogInformation("[Process Message Using Bot ..]");

            if (dto.Message.Type == WhatsappMessageTypes.Text)
            {
                await ProcessTextMessageAsync(dto);
                return;
            }

            if (dto.Message.Type == WhatsappMessageTypes.Interactive)
            {
                if (dto.Message.ActionId is null)
                    throw new NullReferenceException("ActionId is null !");

                await ProcessInteractiveMessageAsync(dto);
                return; 
            }

            _logger.LogError("Invalid message type .");
            throw new InvalidOperationException();
        }

        private async Task ProcessInteractiveMessageAsync(GenarateAnswerDto dto)
        {
            FlowMessageId next = await GetNextMessageAsync(dto);

            var flowMessage = await _context.FlowMessages
                .Include(nameof(FlowMessage.Buttons))
                .Include(nameof(FlowMessage.ListItems))
                .Include(nameof(FlowMessage.Options))
                .SingleAsync(x => x.Id == next);

            await SendFlowMessageAsync(
                dto.ContactInformation.Contact,
                flowMessage
            );

            var processor = _messageProcessorFactory.Create(
             flowMessage.Action, ProcessType.OnSend);

            await processor.HandleAsync(new()
            {
                Contact = dto.ContactInformation.Contact,
                To = dto.Message.From,
            });
        }

        private async Task<FlowMessageId> GetNextMessageAsync(GenarateAnswerDto dto)
        {
            var isButtonReply = dto.Message.InteractiveType == WhatsappInteractiveMessageTypes.ButtonReply;

            if (isButtonReply)
            {
                var flowButton = await _context.FlowMessageButtons
                    .SingleAsync(x => x.Id == new FlowMessageButtonId(Guid.Parse(dto.Message.ActionId!)));

                return flowButton.Next;
            }

            var flowListItem = await _context.FlowMessageListItems
                .SingleAsync(x => x.Id == new FlowMessageListItemId(Guid.Parse(dto.Message.ActionId!)));

            return flowListItem.Next;
        }

        private async Task SendFlowMessageAsync(Contact contact, FlowMessage message , string? content = null)
        {
            _logger.LogInformation("[Send FlowMessaga To Customer ..]");

            var processor = new FlowProcessorFactory(
                _context ,
                contact.Conversation ,
                _whatsappProvider,
                _realtime
            ).Create(message.Type);

            await processor.SendAsync(message, contact.PhoneNumber, content);

            _logger.LogInformation("[FlowMessage Was Sent Successfully]");
        }

        private async Task ProcessTextMessageAsync(GenarateAnswerDto dto)
        {
            var action = await CheckLastMessageActionAsync(dto.ContactInformation.Contact.Conversation.Id);

            var processor = _messageProcessorFactory
                .Create(action ?? FlowMessageAction.None , ProcessType.LastMessage);

            await processor.HandleAsync(new() 
            {
                Contact = dto.ContactInformation.Contact,
                Message = dto.Message.Text , 
                To = dto.Message.From
            });
        }

        private async Task<FlowMessageAction?> CheckLastMessageActionAsync(ConversationId id)
        {
            var message = await _context.Messages
                .Where(x => x.ConversationId == id && !x.Received)
                .Include(x => x.FlowMessage)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            return message?.FlowMessage?.Action;
        }

    }
}
