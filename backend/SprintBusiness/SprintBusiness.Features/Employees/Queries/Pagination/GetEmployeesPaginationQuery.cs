using MediatR;
using SprintBusiness.Domain.Users;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.Users.Queries.Pagination
{
    public class GetEmployeesPaginationQuery(int page , string? keyword = null) : IRequest<PaginationDto<Employee>>
    {
        public int Page { get; set; } = page;
        public string? Keyword { get; set; } = keyword;
    }
}
