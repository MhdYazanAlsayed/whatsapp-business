using SprintBuisness.Contracts.Whatsapp;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBuisness.Whatapp;
using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Flows.FlowMessages.Enums;
using SprintBusiness.Shared.Enums;

namespace SprintBuisness.Application.Bot.FlowProcessors
{
    public class FlowProcessorFactory
    {
        private readonly IWhatsappProvider _whatsappProvider;
        private readonly ApplicationDbContext _context;
        private readonly Conversation _conversation;
        private readonly IRealtime _realTime;

        public FlowProcessorFactory(
            ApplicationDbContext context,
            Conversation conversation,
            IWhatsappProvider whatsappProvider, 
            IRealtime realTime)
        {
            _whatsappProvider = whatsappProvider;
            _context = context;
            _conversation = conversation;
            _realTime = realTime;
        }

        public IFlowProesessor Create (FlowMessageType messageType)
        {
            switch (messageType)
            {
                case FlowMessageType.None:
                    return new NoneFlowProcessor(_whatsappProvider , _realTime , _context, _conversation);
                case FlowMessageType.Buttons:
                    return new ButtonsFlowProcessor(_whatsappProvider, _realTime , _context, _conversation);
                case FlowMessageType.List:
                    return new ListFlowProcessor(_whatsappProvider, _realTime, _context , _conversation);
                case FlowMessageType.Options:
                    return new OptionsFlowProcessor();
            }

            throw new ArgumentOutOfRangeException(nameof(messageType));
        }
    }
}
