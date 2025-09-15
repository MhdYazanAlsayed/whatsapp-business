using SprintBusiness.Domain.Contacts.Keys;

namespace SprintBuisness.Contracts.Whatsapp.Dtos
{
    public class SignalRContactResposne
    {
        public required ContactId Id { get; set; }
        public required string FullName { get; set; }
        public string? NickName { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
