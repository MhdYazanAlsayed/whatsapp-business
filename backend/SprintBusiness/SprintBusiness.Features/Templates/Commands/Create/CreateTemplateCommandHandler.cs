using MediatR;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBuisness.Whatapp;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Contracts.Hangfire;
using SprintBusiness.Features.Templates.Commands.MigrateTemplates;
using SprintBusiness.Domain.Templates;
using SprintBusiness.Domain.Templates.Enums;
using SprintBusiness.Domain.Templates.TemplateComponents.Enums;
namespace SprintBusiness.Features.Templates.Commands.Create
{
    public class CreateTemplateCommandHandler : IRequestHandler<CreateTemplateCommand, ResultDto<List<string>>>
    {
        private readonly IWhatsappProvider _whatsappProvider;
        private readonly ApplicationDbContext _dbContext;
        private readonly IHangfireService _hangfireService;

        public CreateTemplateCommandHandler(
            IWhatsappProvider whatsappProvider,
            IHangfireService hangfireService,
            ApplicationDbContext dbContext)
        {
            _whatsappProvider = whatsappProvider;
            _dbContext = dbContext;
            _hangfireService = hangfireService;
        }

        public async Task<ResultDto<List<string>>> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {
            var result = await _whatsappProvider.CreateTemplateAsync(request);
            
            if (!result.Succeeded)
                return ResultDto<List<string>>.Failure(result.Message);

            // Create a new Template entity
            await CreateTemplateInDatabaseAsync(request);
    
            // Make sure is there a task to check the template status
            // await ScheduleTemplateStatusCheck();

            return ResultDto<List<string>>.Success();

        }

        private async Task CreateTemplateInDatabaseAsync (CreateTemplateCommand request) 
        {
            var template = Template.Create(
                templateId: string.Empty, // Generate a unique ID for the template
                name: request.Name,
                status: TemplateStatus.PENDING, // Initial status
                category: request.Category,
                subCategory: null, // Not provided in the request
                language: request.Language
            );

            // Add components to the template 
            if (request.Header is not null)
            {
                template.AddComponent(request.Header.Type, request.Header.Format ?? TemplateComponentFormat.Text, request.Header.Text);
            }

            if (request.Body is not null)
            {
                template.AddComponent(request.Body.Type, TemplateComponentFormat.Text, request.Body.Text);
            }

            if (request.Buttons is not null && request.Buttons.Buttons is not null)
            {
                foreach (var button in request.Buttons.Buttons)
                {
                    template.AddButton(button.Url, button.Type, button.Text);
                }
            }

            if (request.Footer is not null)
            {
                template.AddComponent(request.Footer.Type, TemplateComponentFormat.Text, request.Footer.Text);
            }
            
            await _dbContext.Templates.AddAsync(template);
            await _dbContext.SaveChangesAsync();
        }
        private async Task ScheduleTemplateStatusCheck()
        {
            var task = await _hangfireService.GetAsync("SyncTemplates");
            if (task == null)
            {   
                await _hangfireService.ScheduleAsync<IMediator>(
                    "SyncTemplates", 
                    TimeSpan.FromMinutes(5), 
                    (mediator) => mediator.Send(new MigrateTemplatesCommand(),default(CancellationToken)));
            }
        }
    }
} 