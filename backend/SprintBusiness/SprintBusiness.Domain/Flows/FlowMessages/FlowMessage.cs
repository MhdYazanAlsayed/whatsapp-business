using SprintBusiness.Domain.Base;
using SprintBusiness.Domain.Flows.FlowMessageButtons;
using SprintBusiness.Domain.Flows.FlowMessageButtons.Dtos;
using SprintBusiness.Domain.Flows.FlowMessageButtons.Keys;
using SprintBusiness.Domain.Flows.FlowMessageItems;
using SprintBusiness.Domain.Flows.FlowMessageItems.Dtos;
using SprintBusiness.Domain.Flows.FlowMessageItems.Keys;
using SprintBusiness.Domain.Flows.FlowMessageLists;
using SprintBusiness.Domain.Flows.FlowMessageLists.Dtos;
using SprintBusiness.Domain.Flows.FlowMessageLists.Keys;
using SprintBusiness.Domain.Flows.FlowMessages.Dtos;
using SprintBusiness.Domain.Flows.FlowMessages.Enums;
using SprintBusiness.Domain.Flows.FlowMessages.Keys;
using SprintBusiness.Domain.Flows.Keys;

namespace SprintBusiness.Domain.Flows.FlowMessages
{
    public class FlowMessage : Entity
    {
        protected FlowMessage()
        {
            Id = null!;
            Content = null!;
        }

        private FlowMessage(CreateFlowMessageDto dto)
        {
            Id = new FlowMessageId(Guid.NewGuid());
            Content = dto.Content;
            Type = dto.Type;
            FlowId = dto.FlowId;
            Number = dto.Number;
            Action = dto.Action;
            EventType = dto.EventType;
        }


        public FlowMessageId Id { get; private set; } 
        public string Content { get; private set; }
        public FlowMessageType Type { get; private set; } // Rename this to TemplateType
        public FlowMessageAction Action { get; private set; }
        public FlowMessageEventType EventType { get; private set; } // Rename this to Type

        public List<FlowMessageButton> Buttons { get; private set; } = new();
        public List<FlowMessageOption> Options { get; private set; } = new();

        public string? ButtonListDisplayText { get; private set; }
        public List<FlowMessageListItem> ListItems { get; private set; } = new();

        public Flow? Flow { get; private set; }
        public FlowId? FlowId { get; private set; }

        public int Number { get; private set; }

        public static FlowMessage Create(CreateFlowMessageDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Content))
                throw new ArgumentNullException(nameof(dto.Content));

            return new FlowMessage(dto);
        }

        public void Update(UpdateFlowMessageDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Content))
                throw new ArgumentNullException(nameof(dto.Content));

            Content = dto.Content;
            FlowId = dto.FlowId;
        }

        public void UpdateListButtonText (string text)
        {
            if (Type != FlowMessageType.List)
                throw new InvalidOperationException();

            ButtonListDisplayText = text;
        }

        public void AddButton(CreateFlowMessageButtonDto dto)
        {
            if (Type != FlowMessageType.Buttons)
                throw new InvalidOperationException();

            dto.MessageId = Id;

            Buttons.Add(FlowMessageButton.Create(dto));
        }

        public void RemoveButton(FlowMessageButtonId id)
        {
            var button = Buttons.Single(x => x.Id == id);

            Buttons.Remove(button);
        }

        public void AddOption(CreateFlowMessageOptionDto dto)
        {
            if (Type != FlowMessageType.Options)
                throw new InvalidOperationException();

            dto.MessageId = Id;

            Options.Add(FlowMessageOption.Create(dto));
        }

        public void RemoveOption(FlowMessageOptionId id)
        {
            var button = Options.Single(x => x.Id == id);

            Options.Remove(button);
        }

        public void AddListItem(CreateFlowMessageListItemDto dto)
        {
            if (Type != FlowMessageType.List)
                throw new InvalidOperationException();

            dto.MessageId = Id;

            var item = FlowMessageListItem.Create(dto);

            ListItems.Add(item);
        }

        public void RemoveListItem(FlowMessageListItemId id)
        {
            var item = ListItems.Single(x => x.Id == id);

            ListItems.Remove(item);
        }

    }
}
