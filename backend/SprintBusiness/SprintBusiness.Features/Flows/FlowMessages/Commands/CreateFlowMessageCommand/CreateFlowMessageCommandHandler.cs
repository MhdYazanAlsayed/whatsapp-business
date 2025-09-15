using MediatR;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Flows.FlowMessages;
using SprintBusiness.Domain.Flows.FlowMessages.Enums;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Helpers;

namespace SprintBusiness.Features.Flows.FlowMessages.Commands.CreateFlowMessageCommand
{
    public class CreateFlowMessageCommandHandler : IRequestHandler<CreateFlowMessagesCommand, ResultDto>
    {
        private readonly ApplicationDbContext _context;
        public CreateFlowMessageCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Handle(CreateFlowMessagesCommand request, CancellationToken cancellationToken)
        {
            var flowMessages = await CreateMessagesAsync(request.Data);

            foreach (var message in request.Data)
            {
                var validation = Validation(message);
                if (!validation.Succeeded) return validation;

                var currentMessage = flowMessages.Single(x => x.MessageId == message.Id);

                if (message.Type == FlowMessageType.Buttons)
                {
                    foreach (var button in message.Buttons!)
                    {
                        var nextMessage = flowMessages.Single(x => x.MessageId == button.Target);

                        currentMessage.FlowMessage.AddButton(new()
                        {
                            DisplayText = button.DisplayText,
                            Next = nextMessage.FlowMessage.Id
                        });

                        _context.FlowMessages.Update(currentMessage.FlowMessage);
                    }

                    continue;
                }

                if (message.Type == FlowMessageType.Options)
                {
                    foreach (var option in message.Options!)
                    {
                        var nextMessage = flowMessages.Single(x => x.MessageId == option.Target);

                        currentMessage.FlowMessage.AddOption(new()
                        {
                            Content = option.Content ,
                            Next = nextMessage.FlowMessage.Id ,
                            Number = option.Number
                        });

                        _context.FlowMessages.Update(currentMessage.FlowMessage);
                    }

                    continue;
                }

                if (message.Type == FlowMessageType.List)
                {
                    foreach (var item in message.ListItems!)
                    {
                        var nextMessage = flowMessages.Single(x => x.MessageId == item.Target);

                        currentMessage.FlowMessage.AddListItem(new()
                        {
                            Content = item.Content ,
                            Next = nextMessage.FlowMessage.Id ,
                            Description = item.Description,
                        });

                        _context.FlowMessages.Update(currentMessage.FlowMessage);
                    }

                    continue;
                }

            }

            await _context.SaveChangesAsync();

            return ResultDto.Success();
        }

        private async Task<List<FlowMessageDto>> CreateMessagesAsync(List<CreateFlowMessageCommandData> messages)
        {
            var idMessages = new List<FlowMessageDto>();
            foreach (var message in messages)
            {
                var flowMessage = FlowMessage.Create(new()
                {
                    Action = message.Action,
                    Content = message.Content,
                    Number = 0 ,
                    Type = message.Type ,
                    EventType = message.EventType
                }) ;

                await _context.FlowMessages.AddAsync(flowMessage);

                idMessages.Add(new (message.Id, flowMessage));
            }

            await _context.SaveChangesAsync();

            return idMessages;
        }

        private ResultDto Validation (CreateFlowMessageCommandData message)
        {
            if (message.Type == FlowMessageType.Buttons && message.Buttons?.Count() == 0)
            {
                // Log error
                return ResultDto.Failure("Buttons is required .");
            }

            if (message.Type == FlowMessageType.Options && message.Options?.Count() == 0)
            {
                // Log error
                return ResultDto.Failure("Options is required .");
            }
            if (message.Type == FlowMessageType.List && (message.ListItems?.Count() == 0 || message.ListButtonDisplayText is null))
            {
                // Log error
                return ResultDto.Failure("ListItems and ListButtonDisplayText are required .");
            }

            return ResultDto.Success();
        }
    }

    public class FlowMessageDto(int messageId , FlowMessage flowMessage)
    {
        // That the user provide us 
        public int MessageId { get; set; } = messageId;
        public FlowMessage FlowMessage { get; set; } = flowMessage;
    }
}
