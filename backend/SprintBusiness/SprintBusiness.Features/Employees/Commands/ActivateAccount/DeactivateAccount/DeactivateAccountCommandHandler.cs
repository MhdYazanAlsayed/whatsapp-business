using MediatR;
using SprintBuisness.Contracts.Authentication;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Helpers;

namespace SprintBusiness.Features.Users.Commands.ActivateAccount.ActivateAccount
{
    public class DeactivateAccountCommandHandler : IRequestHandler<DeactivateAccountCommand, ResultDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorization _authorization;

        public DeactivateAccountCommandHandler(ApplicationDbContext context , IAuthorization authorization)
        {
            _context = context;
            _authorization = authorization;
        }

        public async Task<ResultDto> Handle(DeactivateAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _authorization.GetCurrentUser();
            if (user is null) return ResultDto.Failure();

            user.SetActive(false);

            _context.Update(user);
            await _context.SaveChangesAsync();

            return ResultDto.Success();
        }
    }
}
