using SprintBusiness.Domain.Flows.FlowMessages;

namespace SprintBuisness.Application.Bot.FlowProcessors
{
    public class OptionsFlowProcessor : IFlowProesessor
    {
        public Task SendAsync(FlowMessage message, string to, string? content = null)
        {
            throw new NotImplementedException();
        }
    }
}
