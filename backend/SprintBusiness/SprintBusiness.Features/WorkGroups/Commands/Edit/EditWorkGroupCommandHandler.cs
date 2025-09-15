using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Helpers;

namespace SprintBusiness.Features.WorkGroups.Commands.Edit
{
    public class EditWorkGroupCommandHandler : IRequestHandler<EditWorkGroupCommand, ResultDto>
    {
        private readonly ApplicationDbContext _context;

        public EditWorkGroupCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Handle(EditWorkGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var workGroup = await _context.WorkGroups.SingleAsync(x => x.Id == request.Id);

                workGroup.Update(request.Name);

                _context.Update(workGroup);
                await _context.SaveChangesAsync();

                return ResultDto.Success();
            }
            catch
            {
                return ResultDto.Failure();
            }
        }
    }
}
