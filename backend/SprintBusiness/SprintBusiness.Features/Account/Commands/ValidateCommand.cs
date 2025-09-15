using MediatR;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.Account.Commands
{
    public class ValidateCommand : IRequest<ResultDto<ValidateCommandResult>>
    {
        public ValidateCommand(string code)
        {
            Code = code;
        }

        public string Code { get; set; }
    }
}
