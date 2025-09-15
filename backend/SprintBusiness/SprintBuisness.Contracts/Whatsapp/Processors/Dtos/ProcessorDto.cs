using SprintBusiness.Domain.Contacts;

namespace SprintBuisness.Contracts.Whatsapp.Processors.Dtos
{
    public class ProcessorDto
    {
        public required Contact Contact { get; set; }
        public required string To { get; set; }
        public string? Message { get; set; }
    }
}
