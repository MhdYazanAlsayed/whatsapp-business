using MediatR;
using SprintBusiness.Features.Templates.Commands.SendTemplate.Dtos;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.Home.Commands.SendTemplate
{
    public class SendTemplateApiCommand : IRequest<ResultDto>
    {
        public SendTemplateApiCommand(string phoneNumber , SendTemplateDto dto)
        {
            PhoneNumber = phoneNumber;
            Dto = dto;
        }

        public SendTemplateDto Dto { get; set; }
        public string PhoneNumber { get; set; }
    }
}
