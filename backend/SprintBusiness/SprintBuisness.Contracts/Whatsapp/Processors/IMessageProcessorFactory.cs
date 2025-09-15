using SprintBuisness.Contracts.Markers;
using SprintBuisness.Contracts.Whatsapp.Processors.Enums;
using SprintBusiness.Domain.Flows.FlowMessages.Enums;

namespace SprintBuisness.Contracts.Whatsapp.Processors
{
    public interface IMessageProcessorFactory: IScopedDependency
    {
        IMessageProcessor Create(FlowMessageAction action, ProcessType processType);
    }
}
