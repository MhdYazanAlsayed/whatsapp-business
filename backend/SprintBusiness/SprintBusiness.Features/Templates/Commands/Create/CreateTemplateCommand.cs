
using MediatR;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Whatsapp.Dtos.TemplatesDtos.Create;

namespace SprintBusiness.Features.Templates.Commands.Create
{
    public class CreateTemplateCommand : CreateTemplateDto, IRequest<ResultDto<List<string>>>
    {
    }
} 