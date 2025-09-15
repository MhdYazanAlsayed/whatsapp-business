using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using SprintBuisness.Contracts.Whatsapp;
using SprintBuisness.Contracts.Whatsapp.Dtos;
using SprintBuisness.Contracts.Whatsapp.Dtos.MessageResponse;
using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Shared.Enums;
using SprintBusiness.Shared.Helpers;
using SprintBusiness.Shared.Hubs;
using SprintBusiness.Shared.Hubs.Enums;

namespace SprintBuisness.Application.Bot
{
    public class RealtimeService : IRealtime
    {
        private readonly IHubContext<ChatConversationHub> _chatConversationHub;
        private readonly IMapper _mapper;

        public RealtimeService(IHubContext<ChatConversationHub> hubContext ,
            IMapper mapper)
        {
            _chatConversationHub = hubContext;
            _mapper = mapper;
        }

        public async Task SendMessageAsync(RealtimeMessageDto dto, List<int>? employeeIds = null)
        {
            if (dto.Type == MessageType.FlowMessage && dto.FlowMessage is null)
                throw new InvalidOperationException();

            var response = new SignalRMessageResponse()
            {
                ConversationId = dto.ConversationId,
                Content = dto.Content,
                CreatedAt = dto.CreatedAt,
                IsNotify = dto.IsNotify,
                Type = dto.Type,
                Received = dto.Received,
            };

            if (dto.FlowMessage is not null)
            {
                response.FlowMessage = new SignalRFlowMessageResponse()
                {
                    Action = dto.FlowMessage.Action,
                    Content = dto.FlowMessage.Content,
                    EventType = dto.FlowMessage.EventType,
                    Type = dto.FlowMessage.Type,
                    ButtonListDisplayText = dto.FlowMessage.ButtonListDisplayText,
                    Buttons = dto.FlowMessage.Buttons?.Select(x => new SignalRFlowMessageButtonResponse
                    {
                        DisplayText = x.DisplayText,
                        Id = x.Id,
                        Next = x.Next
                    })
                    .ToList(),
                    ListItems = dto.FlowMessage.ListItems?.Select(x => new SignalRFlowMessageListItemResponse
                    {
                        Content = x.Content,
                        Id = x.Id,
                        Next = x.Next,
                        Description = x.Description
                    })
                    .ToList()
                };
            }

            if (employeeIds is null)
            {
                employeeIds = ConversationManager.GetEmployeesIds(response.ConversationId);
            }

            foreach (var employeeId in employeeIds)
            {
                var connectionId = SignalrManager
                    .GetConnections(HubType.ChatConversation , employeeId);


                await _chatConversationHub.Clients.Clients(connectionId)
                    .SendAsync(RealtimeEvents.NewMessage, response);
            }

            //if (userIds is null)
            //{
            //    //await _hubContext.Clients.All.SendAsync(RealtimeEvents.NewMessage, response);
            //    return;
            //}

            //foreach (var userId in userIds)
            //{
            //    var connectionId = SignalrManager.GetConnections(userId);

            //    foreach (var connection in connectionId)
            //    {
            //        await _hubContext.Clients.Client(connection)
            //            .SendAsync(RealtimeEvents.NewMessage, response);
            //    }
            //}
        }

      

        public async Task UpdateConversationAsync(ConversationUpdateDto dto)
        {
            await _chatConversationHub.Clients.All.SendAsync(RealtimeEvents.Conversations, new 
            {
                Add = dto.Add , 
                Conversation = _mapper.Map<SignalRConversationResponse>(dto.Conversation)
            });
        }
    }
}
