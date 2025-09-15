using MediatR;
using SprintBusiness.Domain.Users.Keys;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.WorkGroups.Commands.Delete
{
    public class DeleteWorkGroupCommand(int id) : IRequest<ResultDto>
    {
        public WorkGroupId Id { get; set; } = new WorkGroupId(id);
    }
}
