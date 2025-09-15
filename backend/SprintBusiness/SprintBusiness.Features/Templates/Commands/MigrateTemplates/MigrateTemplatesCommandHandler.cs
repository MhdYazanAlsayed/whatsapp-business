using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBuisness.Whatapp;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Contracts.Hangfire;
using SprintBusiness.Domain.Templates.Enums;
namespace SprintBusiness.Features.Templates.Commands.MigrateTemplates
{
    public class MigrateTemplatesCommandHandler : IRequestHandler<MigrateTemplatesCommand , ResultDto>
    {
        private readonly IWhatsappProvider _whatsappProvider;
        private readonly ApplicationDbContext _context;
        private readonly IHangfireService _hangfireService;
        public MigrateTemplatesCommandHandler(IWhatsappProvider whatsappProvider ,
            IHangfireService hangfireService,
            ApplicationDbContext context)
        {
            _whatsappProvider = whatsappProvider;
            _context = context;
            _hangfireService = hangfireService;
        }

        public async Task<ResultDto> Handle(MigrateTemplatesCommand request, CancellationToken cancellationToken)
        {
            var templates = await _whatsappProvider.GetAllTemplatesAsync();
            if (!templates.Succeeded) 
                return ResultDto.Failure();

            var existedTemplates = await _context.Templates
                .Include(x => x.Components)
                .ThenInclude(x => x.Variables)
                .Include(x => x.Buttons)
                .ToListAsync();

            foreach (var template in templates.Entity!) 
            {
                var current = existedTemplates.SingleOrDefault(x => x.Name == template.Name);

                if (current is null)
                {
                    await _context.AddAsync(template);
                    continue;
                }
        
                current.Update(template.TemplateId , template.Status , template.Category , template.SubCategory , template.Language);
                current.UpdateComponents(template.Components);
                current.UpdateButtons(template.Buttons);
            }

            await _context.SaveChangesAsync();
            // await DeleteBackgroundJobsWhenThereIsNoPendingTemplate();

            return ResultDto.Success();
        }

        private async Task DeleteBackgroundJobsWhenThereIsNoPendingTemplate()
        {
            var isTherePendingTemplate = await _context.Templates
            .AnyAsync(x => x.Status == TemplateStatus.PENDING);

            if (isTherePendingTemplate)
                return;

            await _hangfireService.DeleteAsync("SyncTemplates");
        }
    }
}
