using MediatR;
using SprintBusiness.Domain.Users.Keys;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.WorkGroups.Commands.Edit
{
    public class EditWorkGroupCommand(int id , string name) : IRequest<ResultDto>
    {
        public WorkGroupId Id { get; set; } = new(id);
        public string Name { get; set; } = name;
    }
}
