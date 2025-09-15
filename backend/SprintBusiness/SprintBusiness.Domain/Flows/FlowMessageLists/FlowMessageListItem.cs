using SprintBusiness.Domain.Base;
using SprintBusiness.Domain.Flows.FlowMessageLists.Dtos;
using SprintBusiness.Domain.Flows.FlowMessageLists.Keys;
using SprintBusiness.Domain.Flows.FlowMessages;
using SprintBusiness.Domain.Flows.FlowMessages.Keys;

namespace SprintBusiness.Domain.Flows.FlowMessageLists
{
    public class FlowMessageListItem : Entity
    {
        protected FlowMessageListItem()
        {
            Content = null!;
            FlowMessageId = null!;
            Description = null;
            Next = null!;
        }
        private FlowMessageListItem(CreateFlowMessageListItemDto dto)
        {
            Id = new FlowMessageListItemId(Guid.NewGuid());
            Content = dto.Content;
            FlowMessageId = dto.MessageId!;
            Description = dto.Description;
            Next = dto.Next;
        }

        public FlowMessageListItemId Id { get; private set; } = null!;
        public string Content { get; private set; }
        public string? Description { get; private set; }

        public FlowMessageId FlowMessageId { get; private set; }
        public FlowMessage? FlowMessage { get; private set; }
        public FlowMessageId Next { get; private set; }

        public static FlowMessageListItem Create (CreateFlowMessageListItemDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Content))
                throw new ArgumentNullException(nameof(dto.Content));

            if (dto.MessageId is null)
                throw new ArgumentNullException(nameof(dto.MessageId));

            return new FlowMessageListItem(dto);
        }

        public void Update (string content , string? description = null)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException(nameof(content));

            Content = content;
            Description = description;
        }
    }
}
