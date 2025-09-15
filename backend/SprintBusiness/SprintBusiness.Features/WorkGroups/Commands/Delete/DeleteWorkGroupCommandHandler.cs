using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Helpers;

namespace SprintBusiness.Features.WorkGroups.Commands.Delete
{
    public class DeleteWorkGroupCommandHandler : IRequestHandler<DeleteWorkGroupCommand, ResultDto>
    {
        private readonly ApplicationDbContext _context;

        public DeleteWorkGroupCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Handle(DeleteWorkGroupCommand request, CancellationToken cancellationToken)
        {
            var workGroup = await _context.WorkGroups.SingleAsync(x => x.Id == request.Id);

            if (workGroup.EmployeesCount > 0)
                return ResultDto.Failure("Cannot delete workgroup contains users .");

            _context.Remove(workGroup);
            await _context.SaveChangesAsync();

            return ResultDto.Success();
        }
    }
}
