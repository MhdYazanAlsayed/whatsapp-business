using MediatR;
using SprintBusiness.Domain.Users.Keys;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.Users.Commands.Create
{
    public class CreateUserCommand : IRequest<ResultDto>
    {
        public CreateUserCommand(string userName , string fullName , string email , string password , string confirmPassword , List<WorkGroupId> workGroupIds)
        {
            UserName = userName;
            FullName = fullName;
            Email = email;  
            Password = password;
            ConfirmPassword = confirmPassword;
            WorkGroupIds = workGroupIds;
        }

        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public List<WorkGroupId> WorkGroupIds { get; set; }
    }
}
