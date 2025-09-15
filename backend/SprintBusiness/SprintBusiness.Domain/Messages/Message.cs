using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SprintBusiness.Domain.Base;
using SprintBusiness.Domain.Base.Interfaces;
using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Conversations.Histories;
using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Flows.FlowMessages;
using SprintBusiness.Domain.Flows.FlowMessages.Keys;
using SprintBusiness.Domain.Messages.DomainEvents;
using SprintBusiness.Domain.Messages.Dtos;
using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Domain.Messages.Keys;
using SprintBusiness.Domain.Messages.Templates;
using SprintBusiness.Domain.Messages.Templates.Keys;
using SprintBusiness.Domain.Users;

namespace SprintBusiness.Domain.Messages
{
    public class Message : Entity , ITrackableTime
    {
        protected Message()
        {
            ConversationId = null!;
        }

        private Message(CreateMessageDto dto)
        {
            ConversationId = dto.ConversationId!;
            Content = dto.Content;
            Received = dto.Received;
            FlowMessageId = dto.FlowMessageId;
            Type = dto.Type;
            FromBot = dto.FromBot;
            SenderId = dto.SenderId;   
            TemplateMessage = dto.TemplateMessage;
        }

        public MessageId Id { get; private set; } = null!;
        public string? Content { get; private set; }
        public bool Received { get; private set; }
        public bool FromBot { get; private set; }
        public int? SenderId { get; private set; }
        public Employee? Sender { get; private set; }

        public ConversationId ConversationId { get; private set; }
        public Conversation? Conversation { get; private set; }


        // Type field specified the type of message 
        // 0 => Default 
        // 1 => FlowMessage 
        // 2 => Template
        public MessageType Type { get; private set; }

        public FlowMessageId? FlowMessageId { get; private set; }
        public FlowMessage? FlowMessage { get; private set; }

        public TemplateMessage? TemplateMessage { get; private set; }
        public TemplateMessageId? TemplateMessageId { get; private set; }

        public bool IsNotify { get; private set; }

        public ConversationHistoryId? HistoryId { get; private set; }
        public ConversationHistory? History { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public static Message CreateReceivedMessage (ConversationId conversationId , string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException();

            return new Message(new CreateMessageDto 
            {
                ConversationId = conversationId,
                FromBot = false ,
                Received = true , 
                Type = MessageType.Text , 
                Content = content ,
            });
        }

        internal static Message CreateTextMessage (
            Conversation conversation , 
            Employee sender , 
            string content )
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException();

            var message = new Message(new CreateMessageDto
            {
                ConversationId = conversation.Id,
                FromBot = false,
                Received = false,
                Type = MessageType.Text,
                Content = content,
                SenderId = sender.Id
            });

            message.AddDomainEvent(new SendTextMessageEvent()
            {
                Content = content ,
                CustomerServiceName = sender.ArabicName ,
                PhoneNumber = conversation.Contact!.PhoneNumber,
            });

            return message;
        }

        internal static Message CreateTemplateMessage(ConversationId conversationId , int senderId , string? header , string? body , string? footer)
        {
            var templateMessage = new TemplateMessage(header , body , footer);

            var message = new Message(new CreateMessageDto
            {
                ConversationId = conversationId,
                Type = MessageType.Template,
                Received = false,
                FromBot = false,
                TemplateMessage = templateMessage ,
                SenderId = senderId 
            });

            // message.AddDomainEvent(new SendTemplate()
            // {
                
            // });

            return message;
        }

        public static Message CreateFlowMessage (ConversationId conversationId , FlowMessageId flowMessageId , string? content = null)
        {
            return new Message(new CreateMessageDto()
            {
                FromBot = true ,
                Received = false , 
                Type = MessageType.FlowMessage,
                FlowMessageId = flowMessageId,
                Content = content,
                ConversationId = conversationId
            });
        }

        internal static Message CreateNotify (ConversationId conversationId , ConversationHistory history)
        {
            var message = new Message()
            {
                Content = null,
                ConversationId = conversationId,
                IsNotify = true,
                History = history,
                FromBot = false
            };

            return message;
        }
    }
}
