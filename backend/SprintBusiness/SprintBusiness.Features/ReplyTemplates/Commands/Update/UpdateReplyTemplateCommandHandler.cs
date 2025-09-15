using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.ReplyTemplates;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.ReplyTemplates.Commands.Update
{
    public class UpdateReplyTemplateCommandHandler : IRequestHandler<UpdateReplyTemplateCommand, ResultDto<ReplyTemplate>>
    {
        private readonly ApplicationDbContext _context;

        public UpdateReplyTemplateCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto<ReplyTemplate>> Handle(UpdateReplyTemplateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ReplyTemplates.SingleOrDefaultAsync(x => x.Id == request.Id);

            if (entity is null) 
                return ResultDto<ReplyTemplate>.Failure();

            entity.Update(request.Title, request.Content);

            _context.Update(entity);
            await _context.SaveChangesAsync();

            return ResultDto<ReplyTemplate>.Success(entity);
        }
    }
}
