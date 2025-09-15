using MediatR;
using SprintBusiness.Domain.ReplyTemplates;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.ReplyTemplates.Commands.Create
{
    public class CreateReplyTemplateCommand : IRequest<ResultDto<ReplyTemplate>>
    {
        public CreateReplyTemplateCommand(string title , string content)
        {
            Title = title;
            Content = content;
        }

        public CreateReplyTemplateCommand(CreateReplyTemplateDto dto)
        {
            Title = dto.Title;
            Content = dto.Content;
        }

        public string Title { get; set; }
        public string Content { get; set; }
    }
}
