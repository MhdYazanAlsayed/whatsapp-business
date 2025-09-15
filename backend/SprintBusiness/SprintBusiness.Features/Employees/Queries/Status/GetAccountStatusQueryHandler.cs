using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SprintBuisness.Contracts.Authentication;
using SprintBuisness.EntityframeworkCore.Contexts;

namespace SprintBusiness.Features.Users.Queries.Status
{
    public class GetAccountStatusQueryHandler : IRequestHandler<GetAccountStatusQuery, GetAccountStatusDto>
    {
        private readonly IAuthorization _authorization;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GetAccountStatusQueryHandler> _logger;

        public GetAccountStatusQueryHandler(IAuthorization authorization , ApplicationDbContext context , ILogger<GetAccountStatusQueryHandler> logger)
        {
            _authorization = authorization;
            _context = context;
            _logger = logger;
        }

        public async Task<GetAccountStatusDto> Handle(GetAccountStatusQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAccountStatusQueryHandler");
            var employeeId = _authorization.GetCurrentEmployeeId();
            _logger.LogInformation("GetAccountStatusQueryHandler. {employeeId}", employeeId);

            if (employeeId is null) throw new NullReferenceException();

            _logger.LogInformation("GetAccountStatusQueryHandler. {employeeId}", employeeId);
            var employee = await _context.Employees.SingleOrDefaultAsync(x => x.Id == employeeId);
            _logger.LogInformation("GetAccountStatusQueryHandler. {employee}", employee);

            if (employee is null) throw new NullReferenceException();

            return new GetAccountStatusDto
            {
                Status = employee.Active
            };
        }
    }
}
