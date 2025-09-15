using MediatR;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.ReplyTemplates;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.ReplyTemplates.Commands.Create
{
    public class CreateReplyTemplateCommandHandler : IRequestHandler<CreateReplyTemplateCommand, ResultDto<ReplyTemplate>>
    {
        private readonly ApplicationDbContext _context;

        public CreateReplyTemplateCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto<ReplyTemplate>> Handle(CreateReplyTemplateCommand request, CancellationToken cancellationToken)
        {
            var replyButton = ReplyTemplate.Create(request.Title, request.Content);

            await _context.AddAsync(replyButton);
            await _context.SaveChangesAsync();

            return ResultDto<ReplyTemplate>.Success(replyButton);
        }
    }
}
