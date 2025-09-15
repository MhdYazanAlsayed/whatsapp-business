using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SprintBuisness.Contracts.Whatsapp;
using SprintBuisness.Contracts.Whatsapp.Dtos;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Contracts.Whatsapp.Dtos;
using SprintBusiness.Domain.Contacts;
using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Messages;
using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Enums;

namespace SprintBusiness.Features.Whatsapp.Webhooks.ReceiveMessage.Handler
{
    public class ReceiveMessageCommandHandler : IRequestHandler<ReceiveMessageCommand, ResultDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IWhatsappBot _whatsappBot;
        private readonly IRealtime _realtime;
        private readonly ILogger<ReceiveMessageCommandHandler> _logger;

        public ReceiveMessageCommandHandler(ApplicationDbContext context,
            IWhatsappBot whatsappBot,
            IRealtime realtime,
            ILogger<ReceiveMessageCommandHandler> logger)
        {
            _context = context;
            _whatsappBot = whatsappBot;
            _realtime = realtime;
            _logger = logger;
        }

        public async Task<ResultDto> Handle(ReceiveMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Start proccessing receive message .");
                var changes = request.Entry.SelectMany(x => x.Changes);

                foreach (var change in changes)
                {
                    // Get all contacts and messages from the change
                    var fullNames = change.Value.Contacts.Select(x => x.Profile.Name).ToList();
                    var phoneNumbers = change.Value.Messages
                        .Select(x => x.From)
                        .Distinct()
                        .ToList();

                    var contacts = await SaveNewContactsAsync(fullNames, phoneNumbers);

                    // Send new conversations to realtime
                    await SendNewConversationsRealtimeAsync(contacts);

                    foreach (var message in change.Value.Messages)
                    {
                        var contact = contacts.Single(x => x.PhoneNumber == message.From);
                        var whatsappMessage = GetMessageFromRequest(message);

                        await SaveMessageAsync(contact.Contact, whatsappMessage.Content);

                        if (contact.Contact.Conversation.Owner != ConversationOwner.Bot)
                            continue;

                        var messageDto = new WhatsappMessageDto()
                        {
                            From = message.From,
                            InteractiveType = message.Interactive?.Type,
                            Text = whatsappMessage.Content,
                            ActionId = whatsappMessage.Id,
                            Type = message.Type,
                        };

                        await _whatsappBot.GenarateAnAnswerAsync(new(contact, messageDto));
                    }
                }

                _logger.LogInformation("Receive message proccessing completed successfully .");

                return ResultDto.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error proccessing receive message :\n" + ex.Message);

                return ResultDto.Failure();
            }
        }

        // TODO: Performance issue here
        private async Task<List<BotContactDto>> SaveNewContactsAsync(List<string> fullNames, List<string> phoneNumbers)
        {
            if (fullNames.Count() != phoneNumbers.Count())
                throw new InvalidOperationException();

            var contacts = new List<BotContactDto>();

            for (var i = 0; i < fullNames.Count(); i++)
            {
                var contact = await FindContactAsync(phoneNumbers[i]);
                if (contact is null)
                {
                    contact = await SaveContactAsync(phoneNumbers[i], fullNames[i]);
                    contacts.Add(
                   new BotContactDto(contact, phoneNumbers[i], fullNames[i], true));

                    continue;
                }

                contacts.Add(
                    new BotContactDto(contact, phoneNumbers[i], fullNames[i], false));
            }

            return contacts;
        }

        private async Task<Contact?> FindContactAsync(string phoneNumber)
        {
            var contact = await _context.Contacts
                .Include(nameof(Contact.Conversation))
                .SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

            return contact;
        }

        private async Task<Contact> SaveContactAsync(string phoneNumber, string fullName)
        {
            var contact = Contact.Create(fullName, phoneNumber);

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        private async Task SaveMessageAsync(Contact contact, string content)
        {
            var message = contact.Conversation.AddReceived(content);
            _context.Contacts.Update(contact);

            await _context.SaveChangesAsync();

            await SendMessageRealTimeAsync(contact.Conversation, message);
        }

        private async Task SendMessageRealTimeAsync(Conversation conversation, Message message)
        {
            List<int>? userIds = null;

            if (conversation.Owner == ConversationOwner.User)
            {
                if (conversation.CustomerServiceEmployeeId is null)
                    throw new NullReferenceException();

                userIds = new List<int>() { conversation.CustomerServiceEmployeeId.Value };
            }

            await _realtime.SendMessageAsync(new()
            {
                ConversationId = message.ConversationId,
                CreatedAt = message.CreatedAt ,
                IsNotify = message.IsNotify ,
                Received = message.Received ,
                Type = message.Type,
                Content = message.Content ,
                FlowMessage = message.FlowMessage
            } , userIds);
        }

        private async Task SendNewConversationsRealtimeAsync(List<BotContactDto> contacts)
        {
            var newContacts = contacts
                    .Where(x => x.IsNew)
                    .Select(x => x.Contact)
                    .ToList();

            foreach (var contact in newContacts)
            {
                await _realtime.UpdateConversationAsync(new()
                {
                    Conversation = contact.Conversation,
                    Add = true
                });
            }
        }

        private (string? Id, string Content) GetMessageFromRequest(MessageResponse message)
        {
            if (message.Type == WhatsappMessageTypes.Text)
                return (null, message.Text!.Body);

            if (message.Type != WhatsappMessageTypes.Interactive)
                throw new NullReferenceException();

            if (message.Interactive!.Type == WhatsappInteractiveMessageTypes.ButtonReply)
            {
                var content = message.Interactive.ButtonReply!.Title;
                var id = message.Interactive.ButtonReply!.Id;

                return (id, content);
            }

            else if (message.Interactive!.Type == WhatsappInteractiveMessageTypes.ListReply)
            {
                var content = message.Interactive.ListReply!.Title;
                var id = message.Interactive.ListReply!.Id;

                return (id, content);
            }

            throw new Exception();
        }

    }
}
