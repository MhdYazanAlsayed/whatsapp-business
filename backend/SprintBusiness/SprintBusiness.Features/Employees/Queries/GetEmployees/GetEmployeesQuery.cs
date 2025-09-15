using MediatR;
using SprintBusiness.Domain.Users;

namespace SprintBusiness.Features.Users.Queries.GetUsers
{
    public class GetEmployeesQuery(string? keyword = null) : IRequest<List<Employee>>
    {
        public string? Keyword { get; set; } = keyword;
    }
}
