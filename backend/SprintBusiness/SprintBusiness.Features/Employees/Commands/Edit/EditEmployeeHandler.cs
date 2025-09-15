using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.Contracts.Authentication;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Helpers;

namespace SprintBusiness.Features.Users.Commands.Edit
{
    public class EditEmployeeHandler : IRequestHandler<EditEmployeeCommand, ResultDto>
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;

        public EditEmployeeHandler(IUserService userService , ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        public async Task<ResultDto> Handle(EditEmployeeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //var user = await _context.Users.SingleAsync(x => x.Id == request.Id);

            //user.Update(request.UserName, request.FullName, request.Email);

            //var result = await _userService.UpdateAsync(user);
            //if (!result.Succeeded)
            //{
            //    var errors = string.Join('\n', result.Errors);

            //    return FcResults.Failure(errors);
            //}

            //return FcResults.Success();
        }
    }
}
