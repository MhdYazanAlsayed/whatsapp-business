using SprintBuisness.Contracts.Whatsapp.Processors;
using SprintBuisness.Contracts.Whatsapp.Processors.Dtos;

namespace SprintBuisness.Application.Bot.MessageProcessors.OnSendProcessors
{
    public class NoneProcessor : IMessageProcessor
    {
        public Task HandleAsync(ProcessorDto dto)
        {
            return Task.CompletedTask;
        }
    }
}
