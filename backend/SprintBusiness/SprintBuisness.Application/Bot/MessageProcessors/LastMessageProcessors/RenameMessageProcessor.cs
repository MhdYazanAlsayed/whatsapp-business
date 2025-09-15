using Microsoft.EntityFrameworkCore;
using SprintBuisness.Application.Bot.FlowProcessors;
using SprintBuisness.Contracts.Database.UnitOfWorks;
using SprintBuisness.Contracts.Whatsapp;
using SprintBuisness.Contracts.Whatsapp.Processors;
using SprintBuisness.Contracts.Whatsapp.Processors.Dtos;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBuisness.Whatapp;
using SprintBusiness.Domain.Flows.FlowMessages;
using SprintBusiness.Domain.Flows.FlowMessages.Enums;

namespace SprintBuisness.Application.Bot.MessageProcessors.LastMessageProcessors
{
    public class RenameMessageProcessor : IMessageProcessor
    {
        private readonly IWhatsappProvider _whatsappProvider;
        private readonly IRealtime _realtime;
        private readonly ApplicationDbContext _context;

        public RenameMessageProcessor(IWhatsappProvider whatsappProvider,
            IRealtime realtime , 
            ApplicationDbContext context)
        {
            _whatsappProvider = whatsappProvider;
            _realtime = realtime;
            _context = context;
        }

        public async Task HandleAsync(ProcessorDto dto)
        {
            if (dto.Message is null)
                throw new ArgumentNullException(nameof(dto.Message));

            dto.Contact.Update(
                dto.Contact.FullName,
                dto.Contact.PhoneNumber,
                dto.Message
            );

            _context.Contacts.Update(dto.Contact);

            var welcomeMessage = await _context.FlowMessages
                .SingleAsync(x => x.EventType == FlowMessageEventType.Welcome);

            var menuMessage = await _context.FlowMessages
                .Include(nameof(FlowMessage.ListItems))
                .SingleAsync(x => x.EventType == FlowMessageEventType.Menu);

            var processor = new FlowProcessorFactory(
                _context, 
                dto.Contact.Conversation,
                _whatsappProvider,
                _realtime).Create(FlowMessageType.List);

            await processor.SendAsync(menuMessage, dto.To,
                welcomeMessage.Content.Replace("{FULLNAME}", dto.Message));
        }
    }
}
