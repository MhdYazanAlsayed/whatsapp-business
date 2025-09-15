using MediatR;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.Users.Commands.Edit
{
    public class EditEmployeeCommand : IRequest<ResultDto>
    {
        public EditEmployeeCommand(string id ,string userName, string englishName, string arabicName, string email)
        {
            Id = id;
            UserName = userName;
            EnglishName = englishName;
            ArabicName = arabicName;
            Email = email;
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string Email { get; set; }
    }
}
