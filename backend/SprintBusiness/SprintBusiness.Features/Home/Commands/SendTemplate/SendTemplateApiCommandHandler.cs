using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBuisness.Whatapp;
using SprintBusiness.Domain.Contacts;
using SprintBusiness.Features.Templates.Commands.SendTemplate;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.Home.Commands.SendTemplate
{
    public class SendTemplateApiCommandHandler : IRequestHandler<SendTemplateApiCommand, ResultDto>
    {
        private readonly IWhatsappProvider _whatsappProvider;
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;

        public SendTemplateApiCommandHandler(
            IWhatsappProvider whatsappProvider,
            IMediator mediator,
            ApplicationDbContext context)
        {
            _whatsappProvider = whatsappProvider;
            _mediator = mediator;
            _context = context;
        }

        public async Task<ResultDto> Handle(SendTemplateApiCommand request, CancellationToken cancellationToken)
        {
            var contact = await _context.Contacts
                .Include(x => x.Conversation)
                .SingleOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);

            if (contact is null)
            {
                var result = await _whatsappProvider.CheckPhoneNumberAsync(request.PhoneNumber);

                if (!result)
                    return ResultDto.Failure("Phone number dose not exist !.");

                contact = Contact.Create("", request.PhoneNumber);
                await _context.AddAsync(contact);
                await _context.SaveChangesAsync();
            }

            var result2 = await _mediator.Send(new SendTemplateCommand(contact.Conversation.Id.Value , request.Dto));

            if (result2.Succeeded)
                return ResultDto.Success();

            return ResultDto.Failure();
        }
    }
}
