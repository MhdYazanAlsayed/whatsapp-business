using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.Contracts.Authentication;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBuisness.Whatapp;
using SprintBusiness.Domain.Messages;
using SprintBusiness.Domain.Templates.Enums;
using SprintBusiness.Domain.Templates.TemplateComponents.Enums;
using SprintBusiness.Features.Templates.Commands.SendTemplate.Dtos;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Whatsapp.Constant.TemplateDtos;
using SprintBusiness.Whatsapp.Dtos.TemplatesDtos.Send;

namespace SprintBusiness.Features.Templates.Commands.SendTemplate
{
    public class SendTemplateCommandHandler : IRequestHandler<SendTemplateCommand, ResultDto<Message>>
    {
        private readonly IWhatsappProvider _whatsappProvider;
        private readonly IMapper _mapper;
        private readonly IAuthorization _authorization;
        private readonly ApplicationDbContext _context;

        public SendTemplateCommandHandler(IWhatsappProvider whatsappProvider,
            IMapper mapper,
            IAuthorization authorization,
            ApplicationDbContext context)
        {
            _whatsappProvider = whatsappProvider;
            _mapper = mapper;
            _authorization = authorization;
            _context = context;
        }

        public async Task<ResultDto<Message>> Handle(SendTemplateCommand request, CancellationToken cancellationToken)
        {
            var employeeId = _authorization.GetCurrentEmployeeId();
            if (employeeId is null)
                throw new UnauthorizedAccessException();

            var conversation = await _context.Conversations
                .Where(x => x.Id == request.ConversationId && x.CustomerServiceEmployeeId == employeeId)
                .Include(x => x.Contact)
                .SingleAsync();

            var template = await _context.Templates
                .Include(x => x.Components)
                .SingleAsync(x => x.Id == request.TemplateId);

            var result = await _whatsappProvider.SendTemplateMessageAsync(new()
            {
                Language = TemplateLanguages.English,
                Name = template!.Name,
                PhoneNumber = conversation.Contact!.PhoneNumber,
                Components =
                _mapper.Map<List<SendTemplateMessageComponent>>(request.Dto.Components)
            });

            if (!result.Succeeded)
                return ResultDto<Message>.Failure(result.Message);

            var bodyOfTemplate = template.Components.Single(x => x.Type == TemplateComponentType.Body).Text!;

            var headerFileName = GetHeaderFromRequest(request.Dto);
            var body = GetBodyFromRequest(request.Dto, bodyOfTemplate);
            var footer = string.Empty;

            var message = conversation.AddTemplateMessage(employeeId.Value,
                    headerFileName , body , footer);

            _context.Update(conversation);
            await _context.SaveChangesAsync();

            return ResultDto<Message>.Success(message);
        }

        private string? GetHeaderFromRequest (SendTemplateDto dto)
        {
            var header = dto.Components
                .SingleOrDefault(x => x.Type == TemplateComponentType.Header);

            if (header is null) 
                return null;

            var parameter = header.Parameters[0];

            if (parameter.Type == TemplateParameterType.Text)
                return parameter.Text;

            if (parameter.Type == TemplateParameterType.Video)
                return parameter.Video?.FileName;

            if (parameter.Type == TemplateParameterType.Image)
                return parameter.Image?.FileName;

            if (parameter.Type == TemplateParameterType.Document)
                return parameter.Document?.FileName;

            throw new NotImplementedException();
        }

        private string GetBodyFromRequest (SendTemplateDto dto , string bodyOfTemplate)
        {
            var body = dto.Components.Single(x => x.Type == TemplateComponentType.Body);

            for (int i = 0; i < body.Parameters.Count; i++)
            {
                bodyOfTemplate = bodyOfTemplate
                    .Replace("{{" + (i + 1) + "}}", body.Parameters[i].Text);
            }

            return bodyOfTemplate;
        }

        //private string? GetFooterFromRequest (SendTemplateDto dto)
        //{
        //    var footer = dto.Components
        //        .SingleOrDefault(x => x.Type == TemplateComponentType.Footer);

        //    if (footer is null)
        //        return null;

        //}
    }
}
