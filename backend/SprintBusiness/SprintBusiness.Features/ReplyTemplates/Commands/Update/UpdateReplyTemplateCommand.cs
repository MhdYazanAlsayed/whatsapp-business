using MediatR;
using SprintBusiness.Domain.ReplyTemplates;
using SprintBusiness.Domain.ReplyTemplates.Keys;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.ReplyTemplates.Commands.Update
{
    public class UpdateReplyTemplateCommand : IRequest<ResultDto<ReplyTemplate>>
    {
        public UpdateReplyTemplateCommand(int id , string title, string content)
        {
            Title = title;
            Content = content;
            Id = new(id);
        }

        public UpdateReplyTemplateCommand(int id , UpdateReplyTemplateDto dto)
        {
            Title = dto.Title;
            Content = dto.Content;
            Id = new(id);
        }

        public ReplyTemplateId Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
