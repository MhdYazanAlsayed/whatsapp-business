using MediatR;
using SprintBusiness.Domain.Conversations.Keys;
using SprintBusiness.Domain.Messages;
using SprintBusiness.Domain.Templates.Keys;
using SprintBusiness.Features.Templates.Commands.SendTemplate.Dtos;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.Templates.Commands.SendTemplate
{
    public class SendTemplateCommand : IRequest<ResultDto<Message>>
    {
        public SendTemplateCommand(int conversationId , SendTemplateDto dto)
        {
            TemplateId = new(dto.Id);
            ConversationId = new(conversationId);
            Dto = dto;
        }

        public TemplateId TemplateId { get; set; }
        public ConversationId ConversationId { get; set; }
        public SendTemplateDto Dto { get; set; }
    }
}
