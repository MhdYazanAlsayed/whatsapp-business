namespace SprintBuisness.Contracts.Whatsapp.Dtos
{
    public class WhatsappMessageDto
    {
        public required string Type { get; set; }
        public required string From { get; set; }
        public required string Text { get; set; }
        public string? InteractiveType { get; set; }
        public string? ActionId { get; set; }
    }
}
