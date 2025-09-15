using SprintBuisness.Contracts.Whatsapp.Dtos;

namespace SprintBusiness.Contracts.Whatsapp.Dtos
{
    public class GenarateAnswerDto(BotContactDto contact , WhatsappMessageDto message)
    {
        public BotContactDto ContactInformation { get; set; } = contact;
        public WhatsappMessageDto Message { get; set; } = message;
    }

}
