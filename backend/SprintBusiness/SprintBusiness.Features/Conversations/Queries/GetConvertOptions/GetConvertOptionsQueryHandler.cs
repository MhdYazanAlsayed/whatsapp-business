using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.Contracts.Authentication;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Messages.Enums;
using SprintBusiness.Domain.Users.Enums;
using SprintBusiness.Domain.Users.Keys;

namespace SprintBusiness.Features.Conversations.Queries.GetConvertOptions
{
    public class GetConvertOptionsQueryHandler : IRequestHandler<GetConvertOptionsQuery, List<ConvertOptionsResultDto>>
    {
        private readonly IAuthorization _authorization;
        private readonly ApplicationDbContext _context;

        public GetConvertOptionsQueryHandler(
            IAuthorization authorization,
            ApplicationDbContext context)
        {
            _authorization = authorization;
            _context = context;
        }

        public async Task<List<ConvertOptionsResultDto>> Handle(GetConvertOptionsQuery request, CancellationToken cancellationToken)
        {
            var employeeId = _authorization.GetCurrentEmployeeId();
            var employee = await _context.Employees
                .Include(x => x.WorkGroups)
                .ThenInclude(x => x.Employees)
                .SingleAsync(x => x.Id == employeeId);

            var options = new List<ConvertOptionsResultDto>()
            {
                new ConvertOptionsResultDto()
                {
                    Text = "البوت",
                    Type = ConversationOwner.Bot ,
                    Id = "-1"
                },
                new ConvertOptionsResultDto()
                {
                    Text = "قسم خدمة العملاء",
                    Type = ConversationOwner.CustomerService ,
                    Id = "-2"
                }
            };

            WorkGroupId? workGroupId = null;
            ConversationOwner? owner = null;

            if (request.Id is not null)
            {
                var conversation = await _context.Conversations
                    .SingleAsync(x => x.Id == request.Id);

                owner = conversation.Owner;
                workGroupId = conversation.WorkGroupId;
            }
            else
            {
                owner = ConversationOwner.CustomerService;
            }

            // Remove the current option
            var currentOption = options.SingleOrDefault(x => x.Type == owner);
            if (currentOption is not null)
            {
                options.Remove(currentOption);
            }

            if (employee.Type == UserType.SuperAdmin)
            {
                // TODO: Add cache for this query
                var allUsers = await _context.Employees
                    .Where(x => x.Type != UserType.SuperAdmin)
                    .ToListAsync();

                // TODO: Add cache for this query
                var allGroups = await _context.WorkGroups.ToListAsync();

                options.AddRange(allUsers.Select(x => new ConvertOptionsResultDto
                {
                    Id = x.Id.ToString(),
                    Text = x.EnglishName ,
                    Type = ConversationOwner.User ,
                }));
                options.AddRange(allGroups.Select(x => new ConvertOptionsResultDto
                {
                    Id = x.Id.ToString(),
                    Text = x.Name,
                    Type = ConversationOwner.WorkGroup,
                }));

                return options;
            }

            var mates = employee.WorkGroups
                .SelectMany(x => x.Employees)
                .Distinct()
                .Where(x => x.Id != employeeId)
                .ToList();

            var myWorkGroups = employee.WorkGroups
                .Where(x => x.Id != workGroupId)    
                .Select(x => new ConvertOptionsResultDto
                {
                    Id = x.Id.ToString(),
                    Text = x.Name,
                    Type = ConversationOwner.WorkGroup,
                });

            options.AddRange(mates.Select(x => new ConvertOptionsResultDto
            {
                Id = x.Id.ToString(),
                Text = x.EnglishName,
                Type = ConversationOwner.User,
            }));
            options.AddRange(myWorkGroups);

            return options;
        }
    }
}
