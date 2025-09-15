using SprintBusiness.Domain.Flows.FlowMessages;

namespace SprintBuisness.Application.Bot.FlowProcessors
{
    public interface IFlowProesessor
    {
        Task SendAsync (FlowMessage message, string to , string? content = null);
    }
}
