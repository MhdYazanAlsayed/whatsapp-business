using MediatR;
using SprintBuisness.Contracts.Authentication;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.Users.Commands.ActivateAccount.ActivateAccount
{
    public class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand, ResultDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorization _authorization;

        public ActivateAccountCommandHandler(ApplicationDbContext context, IAuthorization authorization)
        {
            _context = context;
            _authorization = authorization;
        }

        public async Task<ResultDto> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _authorization.GetCurrentUser();
            if (user is null) return ResultDto.Failure();

            user.SetActive(true);

            _context.Update(user);
            await _context.SaveChangesAsync();

            return ResultDto.Success();
        }  
    }
}
