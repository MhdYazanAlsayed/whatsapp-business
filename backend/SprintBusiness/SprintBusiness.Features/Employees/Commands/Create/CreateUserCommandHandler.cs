using MediatR;
using SprintBuisness.Contracts.Authentication;
using SprintBusiness.Domain.Users;
using SprintBusiness.Domain.Users.Enums;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Helpers;

namespace SprintBusiness.Features.Users.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResultDto>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResultDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //if (request.Password.Trim() != request.ConfirmPassword.Trim())
            //    return FcResults.Failure("Passwords don't match .");

            //var user = ApplicationUser.Create(
            //    request.UserName , 
            //    request.FullName ,
            //    request.Email ,
            //    UserType.User);

            //var result = await _userService.RegisterAsync(user, request.Password);
            //if (!result.Succeeded)
            //{
            //    var errors = string.Join('\n', result.Errors);
            //    return FcResults.Failure(errors);
            //}

            //return FcResults.Success();
        }
    }
}
