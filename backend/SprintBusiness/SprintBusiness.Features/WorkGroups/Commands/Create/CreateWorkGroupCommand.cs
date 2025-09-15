using MediatR;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.WorkGroups.Commands.Create
{
    public class CreateWorkGroupCommand(string name) : IRequest<ResultDto>
    {
        public string Name { get; set; } = name;
    }
}
