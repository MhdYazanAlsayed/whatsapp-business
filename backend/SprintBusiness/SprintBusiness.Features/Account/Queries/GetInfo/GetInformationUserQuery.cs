using MediatR;
using SprintBusiness.Domain.Users;

namespace SprintBusiness.Features.Account.Queries.GetInfo
{
    public class GetInformationUserQuery : IRequest<Employee?>
    {
        public GetInformationUserQuery(string? accessToken = null)
        {
            AccessToken = accessToken;
        }

        public string? AccessToken { get; set; }
    }
}
