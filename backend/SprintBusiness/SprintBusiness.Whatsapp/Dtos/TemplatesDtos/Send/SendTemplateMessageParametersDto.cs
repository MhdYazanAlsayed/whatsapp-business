namespace SprintBusiness.Whatsapp.Dtos.TemplatesDtos.Send
{
    public class SendTemplateMessageParametersDto
    {
        public required string Type { get; set; }
        public string? Text { get; set; }
        public SendTemplateMessageCurrencyParametersDto? Currency { get; set; }
        public SendTemplateMessageDateTimeParametersDto? DateTime { get; set; }
        public SendTemplateMessageFileDto? Document { get; set; }
        public SendTemplateMessageFileDto? Image { get; set; }
        public SendTemplateMessageFileDto? Video { get; set; }
        public SendTemplateMessageFileDto? Audio { get; set; }
    }
}
