using SprintBusiness.Domain.Base;
using SprintBusiness.Domain.Contacts.Keys;
using SprintBusiness.Domain.Conversations;

namespace SprintBusiness.Domain.Contacts
{
    public class Contact : Entity
    {
        protected Contact () 
        {
            Id = new ContactId(0);
            FullName = string.Empty;
            PhoneNumber = string.Empty;
            NickName = null;
            Conversation = Conversation.Create(Id);
         }
        private Contact(string fullName, string phoneNumber, string? nickName = null)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            NickName = nickName;
            Conversation = Conversation.Create(Id);
        }

        public ContactId Id { get; private set; } = null!;
        public string FullName { get; private set; }
        public string? NickName { get; private set; }
        public string PhoneNumber { get; private set; }
        public Conversation Conversation { get; private set; } = null!;

        public static Contact Create(string fullName, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException();

            return new Contact(fullName, phoneNumber);
        }

        public void Update(string fullName, string phoneNumber, string? nickName = null)
        {
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException();

            FullName = fullName;
            PhoneNumber = phoneNumber;
            NickName = nickName;
        }
    }
}
