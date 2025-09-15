using Microsoft.EntityFrameworkCore;
using SprintBuisness.Contracts.Database.UnitOfWorks;
using SprintBuisness.Contracts.Whatsapp;
using SprintBuisness.Contracts.Whatsapp.Processors;
using SprintBuisness.Contracts.Whatsapp.Processors.Dtos;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBuisness.Whatapp;

namespace SprintBuisness.Application.Bot.MessageProcessors.OnSendProcessors
{
    public class ConvertToCustomerServiceProcessor : IMessageProcessor
    {
        private readonly IRealtime _realtime;
        private readonly ApplicationDbContext _context;

        public ConvertToCustomerServiceProcessor(
            IWhatsappProvider whatsappProvider,
            IRealtime realtime , 
            ApplicationDbContext context)
        {
            _realtime = realtime;
            _context = context;
        }

        public async Task HandleAsync(ProcessorDto dto)
        {
            var conversation = await _context.Conversations
                .SingleAsync(x => x.Id == dto.Contact.Conversation.Id);

            conversation.ConvertToCustomerService();

            _context.Conversations.Update(conversation);
            await _context.SaveChangesAsync();

            await _realtime.UpdateConversationAsync(new()
            {
                Add = false,
                Conversation = conversation
            });
        }
    }
}
