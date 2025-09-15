using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Conversations.Histories;
using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Messages;
using SprintBusiness.Domain.Users.Dtos;
using SprintBusiness.Domain.Users.Enums;
using SprintBusiness.Domain.Users.Keys;
using SprintBusiness.Domain.Users.WorkGroups;

namespace SprintBusiness.Domain.Users
{
    public class Employee 
    {
        protected Employee()
        {
            EnglishName = null!;
            Email = null!;
            UserId = null!;
            ArabicName = null!;
        }

        private Employee(int id , string userId , string englishName, string arabicName, string email, UserType type , bool active = false)
        {
            Id = id;
            EnglishName = englishName;
            Email = email;
            Type = type;
            Active = active;
            ArabicName = arabicName;
            UserId = userId;
        }

        public int Id { get; private set; } 
        public string EnglishName { get; private set; }
        public string ArabicName { get; private set; }
        public string Email { get; set; }
        public bool Active { get; private set; }
        public int WorkGroupsCount { get; private set; }
        public UserType Type { get; private set; }
        public string UserId { get; set; }

        public List<Conversation> Conversations { get; private set; } = new();
        public List<ConversationHistory> ConversationHistories { get; private set; } = new();
        public List<WorkGroup> WorkGroups { get; private set; } = new();
        //public List<ReplyTemplate> ReplyTemplates { get; private set; } = new();

        public static Employee Create (int id , string userId , string arabicName , string englishName , string email , UserType type , bool active = false)
        {
            if (string.IsNullOrWhiteSpace(arabicName) || string.IsNullOrWhiteSpace(englishName))
                throw new ArgumentNullException();

            return new Employee(id , userId , englishName , arabicName , email, type , active);
        }

        public void Update(string englishName , string arabicName , string email)
        {
            if (string.IsNullOrWhiteSpace(englishName) || string.IsNullOrWhiteSpace(arabicName) || string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException();

            EnglishName = englishName;
            ArabicName = arabicName;
            Email = email;
        }

        public void SetActive (bool active)
        {
            Active = active;
        }

        public void TakeConversation (Conversation conversation)
        {
            conversation.TakeConversation(Id);

            Conversations.Add(conversation);
        }

        public void ConvertConversation (ConvertUserConversationDto dto)
        {
            var conversation = Conversations.Single(x => x.Id == dto.ConversationId);

            conversation.Convert(new() 
            {
                To = dto.To,
                EmployeeId = Id,
                RecipientId = dto.RecipientId ,
                WorkGroupId = dto.WorkGroupId ,
            });
        }

        public void AddWorkGroup (List<WorkGroup> workGroups)
        {
            WorkGroups.AddRange(workGroups);
            WorkGroupsCount++;
        }

        public void RemoveWorkGroup (WorkGroupId id)
        {
            var group = WorkGroups.Single(x => x.Id == id);

            WorkGroups.Remove(group);
            WorkGroupsCount--;
        }

        public void SendMessage (ConversationId conversationId , string content)
        {
            var conversation = Conversations.Single(x => x.Id == conversationId);

            conversation.AddTextMessage(this , content);
        }

        public Message SendTemplateMessage (ConversationId conversationId , string? headerFileName , string body , string? footer)
        {
            var conversation = Conversations.Single(x => x.Id == conversationId);

            return conversation.AddTemplateMessage(Id , headerFileName , body , footer);
        }
    }
}
