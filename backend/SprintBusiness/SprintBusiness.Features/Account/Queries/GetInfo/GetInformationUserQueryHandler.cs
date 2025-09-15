using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.Contracts.Authentication;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Users;
using System.Security.Claims;

namespace SprintBusiness.Features.Account.Queries.GetInfo
{
    public class GetInformationUserQueryHandler : IRequestHandler<GetInformationUserQuery, Employee?>
    {
        private readonly ApplicationDbContext _context;
        private readonly IToken _tokenService;
        private readonly IAuthorization _authorization;

        public GetInformationUserQueryHandler(ApplicationDbContext context, IToken tokenService , 
        IAuthorization authorization)
        {
            _context = context;
            _tokenService = tokenService;
            _authorization = authorization;
        }

        public async Task<Employee?> Handle(GetInformationUserQuery request, CancellationToken cancellationToken)
        {
            var employeeId = _authorization.GetCurrentEmployeeId(request.AccessToken);
            if (employeeId is null) throw new NullReferenceException();

            return await _context.Employees.SingleOrDefaultAsync(x => x.Id == employeeId);
        }
    }
}
