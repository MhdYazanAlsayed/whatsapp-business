using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.Contracts.Authentication;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Users;
using SprintBusiness.Domain.Users.Enums;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Helpers;

namespace SprintBusiness.Features.Users.Commands.EditPassword
{
    public class EditPasswordCommandHandler : IRequestHandler<EditPasswordCommand, ResultDto>
    {
        private readonly IUserService _userService;
        private readonly IAuthorization _authorization;
        private readonly ApplicationDbContext _context;

        public EditPasswordCommandHandler(
            IUserService userService , 
            IAuthorization authorization,
            ApplicationDbContext context)
        {
            _userService = userService;
            _authorization = authorization;
            _context = context;
        }

        public async Task<ResultDto> Handle(EditPasswordCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //var currentUser = await _authorization.GetLoggedUserAsync();
            //if (currentUser is null)
            //    return FcResults.Failure("UnAuthorized exception ..");

            //if (request.Password.Trim() != request.ConfirmPassword.Trim())
            //    return FcResults.Failure("Passwords do not match .");

            //if (request.UserId is not null)
            //{
            //    return await ChangePasswordForAdminAsync(
            //        currentUser, request.UserId, request.Password);
            //}

            //return await ChangePasswordAsync(currentUser , request.Password);
        }

        //private async Task<ResultDto> ChangePasswordAsync (ApplicationUser currentUser , string password)
        //{
        //    currentUser.PasswordHash = _userService.HashPassword(currentUser, password);
        //    var result = await _userService.UpdateAsync(currentUser);
            
        //    if (!result.Succeeded)
        //    {
        //        var errors = string.Join('\n' , result.Errors);

        //        return FcResults.Failure(errors);
        //    }

        //    return FcResults.Success();
        //}

        //private async Task<ResultDto> ChangePasswordForAdminAsync (ApplicationUser currentUser , string userId , string password)
        //{
        //    if (currentUser.Type != UserType.SuperAdmin)
        //    {
        //        return FcResults.Failure("You do not have permission to change password for another user .");
        //    }

        //    var user = await _context.Users.SingleAsync(x => x.Id == userId);

        //    user.PasswordHash = _userService
        //            .HashPassword(user, password);

        //    var result = await _userService.UpdateAsync(user);
        //    if (!result.Succeeded)
        //    {
        //        var errors = string.Join('\n', result.Errors);

        //        return FcResults.Failure(errors);
        //    }

        //    return FcResults.Success();
        //}
    }
}
