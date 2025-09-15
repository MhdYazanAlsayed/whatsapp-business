using MediatR;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Users.WorkGroups;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Helpers;

namespace SprintBusiness.Features.WorkGroups.Commands.Create
{
    public class CreateWorkGroupCommandHandler : IRequestHandler<CreateWorkGroupCommand, ResultDto>
    {
        private readonly ApplicationDbContext _context;

        public CreateWorkGroupCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Handle(CreateWorkGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var workGroup = WorkGroup.Create(request.Name);

                await _context.AddAsync(workGroup);
                await _context.SaveChangesAsync();

                return ResultDto.Success();
            }
            catch (Exception ex)
            {
                return ResultDto.Failure(ex.Message);
            }
        }
    }
}
