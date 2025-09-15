using MediatR;
using SprintBusiness.Domain.Flows.FlowMessages.Enums;
using SprintBusiness.Shared.Dtos;
using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Features.Flows.FlowMessages.Commands.CreateFlowMessageCommand
{
    public class CreateFlowMessagesCommand : IRequest<ResultDto>
    {
        [Required]
        public List<CreateFlowMessageCommandData> Data { get; set; }

    }

    public class CreateFlowMessageCommandData
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public FlowMessageAction Action { get; set; }

        [Required]
        public FlowMessageType Type { get; set; }

        [Required]
        public FlowMessageEventType EventType { get; set; }

        public List<CreateFlowButtonCommand>? Buttons { get; set; }

        public List<CreateFlowOptionCommand>? Options { get; set; }

        public string? ListButtonDisplayText { get; set; }
        public List<CreateFlowListItemCommand>? ListItems { get; set; }

    }

    public class CreateFlowButtonCommand
    {
        [Required]
        public int Target { get; set; }

        [Required]
        public string DisplayText { get; set; }
    }

    public class CreateFlowOptionCommand
    {
        [Required]
        public int Target { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int Number { get; set; }
    }

    public class CreateFlowListItemCommand
    {
        [Required]
        public int Target { get; set; }

        [Required]
        public string Content { get; set; }

        public string? Description { get; set; }
    }
}
