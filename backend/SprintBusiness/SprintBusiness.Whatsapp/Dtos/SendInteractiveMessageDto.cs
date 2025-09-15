namespace SprintBusiness.Whatsapp.Dtos
{
    public class SendInteractiveMessageDto
    {
        public required string To { get; set; }
        public required string Message { get; set; }
        public required List<InteractiveMessageButton> Buttons { get; set; }
    }
}
