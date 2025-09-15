using SprintBusiness.Domain.Contacts;

namespace SprintBusiness.Contracts.Whatsapp.Dtos
{
    public class BotContactDto(Contact contact, string phoneNumber, string fullName, bool isNew)
    {
        public Contact Contact { get; set; } = contact;
        public string PhoneNumber { get; set; } = phoneNumber;
        public string FullName { get; set; } = fullName;
        public bool IsNew { get; set; } = isNew;
    }
}
