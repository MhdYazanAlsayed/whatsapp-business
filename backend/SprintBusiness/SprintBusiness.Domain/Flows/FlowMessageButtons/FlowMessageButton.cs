using SprintBusiness.Domain.Base;
using SprintBusiness.Domain.Flows.FlowMessageButtons.Dtos;
using SprintBusiness.Domain.Flows.FlowMessageButtons.Keys;
using SprintBusiness.Domain.Flows.FlowMessages;
using SprintBusiness.Domain.Flows.FlowMessages.Keys;

namespace SprintBusiness.Domain.Flows.FlowMessageButtons
{
    public class FlowMessageButton : Entity
    {
        protected FlowMessageButton()
        {
            DisplayText = null!;
            Next = null!;
            FlowMessageId = null!;
        }

        private FlowMessageButton(string displayText, FlowMessageId next, FlowMessageId flowMessageId)
        {
            Id = new FlowMessageButtonId(Guid.NewGuid());
            DisplayText = displayText;
            Next = next;
            FlowMessageId = flowMessageId;
        }

        public FlowMessageButtonId Id { get; private set; } = null!;

        public string DisplayText { get; private set; }
        public FlowMessageId Next { get; private set; }

        public FlowMessage? FlowMessage { get; private set; }
        public FlowMessageId FlowMessageId { get; private set; } = null!;

        public static FlowMessageButton Create(CreateFlowMessageButtonDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.DisplayText))
                throw new ArgumentNullException(nameof(dto.DisplayText));

            if (dto.MessageId is null)
                throw new ArgumentNullException(nameof(dto.MessageId));

            return new FlowMessageButton(dto.DisplayText, dto.Next , dto.MessageId);
        }

        public void Update(string displayText, FlowMessageId next)
        {
            if (string.IsNullOrWhiteSpace(displayText))
                throw new ArgumentNullException(nameof(displayText));

            if (displayText.Length > 20)
                throw new ArgumentOutOfRangeException("DisplayText must be least than 20 .");

            DisplayText = displayText;
            Next = next;
        }

    }
}
