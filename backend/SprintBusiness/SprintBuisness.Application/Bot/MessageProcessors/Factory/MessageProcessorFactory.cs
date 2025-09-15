using SprintBuisness.Application.Bot.MessageProcessors.LastMessageProcessors;
using SprintBuisness.Application.Bot.MessageProcessors.OnSendProcessors;
using SprintBuisness.Contracts.Whatsapp;
using SprintBuisness.Contracts.Whatsapp.Processors;
using SprintBuisness.Contracts.Whatsapp.Processors.Enums;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBuisness.Whatapp;
using SprintBusiness.Domain.Flows.FlowMessages.Enums;

namespace SprintBuisness.Application.Bot.MessageProcessors.Factory
{
    public class MessageProcessorFactory : IMessageProcessorFactory
    {
        private readonly IWhatsappProvider _whatsappProvider;
        private readonly IRealtime _realtime;
        private readonly ApplicationDbContext _context;

        public MessageProcessorFactory(IWhatsappProvider whatsappProvider,
            IRealtime realtime , 
            ApplicationDbContext context)
        {
            _whatsappProvider = whatsappProvider;
            _realtime = realtime;
            _context = context;
        }

        public IMessageProcessor Create(FlowMessageAction action, ProcessType processType)
        {
            if (action == FlowMessageAction.None && processType == ProcessType.LastMessage)
                return new UnkownMessageProcessor(_whatsappProvider, _realtime , _context);

            if (action == FlowMessageAction.ReName && processType == ProcessType.LastMessage)
                return new RenameMessageProcessor(_whatsappProvider, _realtime, _context);

            if (action == FlowMessageAction.ConvertToCustomerService &&
                processType == ProcessType.OnSend)
                return new ConvertToCustomerServiceProcessor(_whatsappProvider, _realtime, _context);

            return new NoneProcessor();
        }
    }
}
