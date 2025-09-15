using SprintBuisness.Contracts.Whatsapp.Processors.Dtos;

namespace SprintBuisness.Contracts.Whatsapp.Processors
{
    public interface IMessageProcessor
    {
        Task HandleAsync(ProcessorDto dto);
    }
}
