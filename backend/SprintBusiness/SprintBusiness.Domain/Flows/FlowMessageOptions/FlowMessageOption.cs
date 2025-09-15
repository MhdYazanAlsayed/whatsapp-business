using SprintBusiness.Domain.Base;
using SprintBusiness.Domain.Flows.FlowMessageItems.Dtos;
using SprintBusiness.Domain.Flows.FlowMessageItems.Keys;
using SprintBusiness.Domain.Flows.FlowMessages;
using SprintBusiness.Domain.Flows.FlowMessages.Keys;

namespace SprintBusiness.Domain.Flows.FlowMessageItems
{
    public class FlowMessageOption : Entity
    {
        protected FlowMessageOption()
        {
            Content = null!;
            Number = 0;
            FlowMessageId = null!;
            Next = null!;
        }
        private FlowMessageOption(string content, int number, FlowMessageId flowMessageId, FlowMessageId next)
        {
            Content = content;
            Number = number;
            FlowMessageId = flowMessageId;
            Next = next;
        }

        public FlowMessageOptionId Id { get; private set; } = null!;
        public string Content { get; private set; }
        public int Number { get; private set; }

        public FlowMessageId FlowMessageId { get; private set; }
        public FlowMessage? FlowMessage { get; private set; }

        public FlowMessageId Next { get; private set; }

        public static FlowMessageOption Create(CreateFlowMessageOptionDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Content) || dto.MessageId is null)
                throw new ArgumentNullException();

            return new FlowMessageOption(dto.Content, dto.Number, dto.MessageId, dto.Next);
        }

        public void Update(UpdateFlowMessageOptionDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Content))
                throw new ArgumentNullException();

            Content = dto.Content;
            Next = dto.Next;
        }
    }
}
