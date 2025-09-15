namespace SprintBusiness.Whatsapp.Dtos.TemplatesDtos.Send
{
    public class SendTemplateMessageDto
    {
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Language { get; set; }
        public List<SendTemplateMessageComponent>? Components { get; set; }
    }
}
