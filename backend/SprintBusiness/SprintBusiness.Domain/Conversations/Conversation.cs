using SprintBusiness.Domain.Base;
using SprintBusiness.Domain.Base.Interfaces;
using SprintBusiness.Domain.Contacts;
using SprintBusiness.Domain.Contacts.Keys;
using SprintBusiness.Domain.Conversations.Dtos;
using SprintBusiness.Domain.Conversations.Histories;
using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Conversations.Notes;
using SprintBusiness.Domain.Flows.FlowMessages.Keys;
using SprintBusiness.Domain.Messages;
using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Domain.Users;
using SprintBusiness.Domain.Users.Keys;
using SprintBusiness.Domain.Users.WorkGroups;

namespace SprintBusiness.Domain.Conversations
{
    public class Conversation : Entity , ITrackableTime 
    {
        protected Conversation()
        {
            ContactId = null!;
        }

        private Conversation(ContactId contactId)
        {
            ContactId = contactId;
            Owner = ConversationOwner.Bot;
        }

        public ConversationId Id { get; private set; } = null!;

        public ContactId ContactId { get; private set; }
        public Contact? Contact { get; private set; }

        public ConversationOwner Owner { get; private set; }

        public int? CustomerServiceEmployeeId { get; private set; }
        public Employee? CustomerServiceEmployee { get; private set; }

        public WorkGroupId? WorkGroupId { get; private set; }
        public WorkGroup? WorkGroup { get; private set; }

        public List<Message> Messages { get; private set; } = new();
        public List<ConversationHistory> Histories { get; private set; } = new();

        public ConversationNote? Note { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public static Conversation Create(ContactId contactId)
        {
            return new Conversation(contactId);
        }

        // Users actions
        internal void TakeConversation (int employeeId)
        {
            if (Owner == ConversationOwner.User)
                throw new InvalidOperationException("Cannot take this conversation because it is already taken by another employee.");

            Owner = ConversationOwner.User;
            CustomerServiceEmployeeId = employeeId;

            AddNotify(new CreateConversationHistoryDto()
            {
                ConversationId = Id,
                CurrentOwner = Owner,
                EmployeeId = employeeId
            });
        }

        public void Convert (ConvertConversationDto dto)
        {
            // Can't any one convert a conversation that is taken by another user
            if (Owner == ConversationOwner.User)
            {   
                // Check if this conversation is taken by another employee
                // EmployeeId is null when the conversation is taken by the bot
                // EmployeeId should not be diffrent from the customer service employee id
                if (dto.EmployeeId is null || dto.EmployeeId != CustomerServiceEmployeeId) 
                    throw new InvalidOperationException("Cannot convert this conversation because it is already taken by another employee.");
            }

            if (dto.To == ConversationOwner.Bot || dto.To == ConversationOwner.CustomerService)
            {
                dto.RecipientId = null;
                dto.WorkGroupId = null;
            }
            else if (dto.To == ConversationOwner.WorkGroup)
            {
                if (dto.WorkGroupId is null)
                    throw new ArgumentNullException(nameof(dto.WorkGroupId));

                dto.RecipientId = null;
            }
            else if (dto.To == ConversationOwner.User)
            {
                if (dto.RecipientId is null)
                    throw new ArgumentNullException(nameof(dto.RecipientId));

                dto.WorkGroupId = null;
            }

            Owner = dto.To;
            CustomerServiceEmployeeId = dto.RecipientId;
            WorkGroupId = dto.WorkGroupId;

            AddNotify(new CreateConversationHistoryDto()
            {
                ConversationId = Id,
                CurrentOwner = dto.To,
                WorkGroupId = dto.WorkGroupId,
                EmployeeId = dto.RecipientId
            });
        }

        public void ConvertToCustomerService()
        {
            Owner = ConversationOwner.CustomerService;
            CustomerServiceEmployeeId = null;
            WorkGroupId = null;

            AddNotify(new() 
            {
                ConversationId = Id,
                CurrentOwner = ConversationOwner.CustomerService ,
            });
        }

        public Message AddReceived(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException();

            var message = Message.CreateReceivedMessage(Id , content);

            Messages.Add(message);

            return message;
        }

        // // Called only from user
        internal Message AddTextMessage (Employee employee , string content)
        {
            var message = Message.CreateTextMessage(this , employee , content);

            Messages.Add(message);

            return message;
        }

        public Message AddTemplateMessage(int senderId , string? headerFileName , string body , string? footer)
        {
            var message = Message
                .CreateTemplateMessage(Id , senderId , headerFileName , body , footer);

            Messages.Add(message);

            return message;
        }

        // // Called only from bot
        public Message AddFlowMessage(FlowMessageId flowMessageId , string? content = null)
        {
            var message = Message.CreateFlowMessage(Id , flowMessageId , content);

            Messages.Add(message);

            return message;
        }

        // public void AddNote(CreateConversationNoteDto dto)
        // {
        //     if (Note is not null)
        //         throw new InvalidOperationException();

        //     var conversationNote = new ConversationNote(Id, dto.Content);
        //     conversationNote.AddAttachments(dto.Attachments);

        //     Note = conversationNote;
        // }

        private void AddNotify(CreateConversationHistoryDto dto)
        {
            var history =
                 ConversationHistory.Create(dto);

            Histories.Add(history);

            var notify = Message.CreateNotify(Id, history);
            Messages.Add(notify);
        }
    }

}
