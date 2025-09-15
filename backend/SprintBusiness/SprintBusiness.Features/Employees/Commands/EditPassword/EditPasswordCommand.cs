using MediatR;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.Users.Commands.EditPassword
{
    public class EditPasswordCommand(string password , string confirmPassword, string? userId = null) : IRequest<ResultDto>
    {
        public string? UserId { get; set; } = userId;
        public string Password { get; set; } = password;
        public string ConfirmPassword { get; set; } = confirmPassword;
    }
}
