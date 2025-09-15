using MediatR;
using SprintBusiness.Domain.Users;

namespace SprintBusiness.Features.Users.Queries.Details
{
    public class GetEmployeeDetailsQuery(int id) : IRequest<Employee?>
    {
        public int Id { get; set; } = id;
    }
}
