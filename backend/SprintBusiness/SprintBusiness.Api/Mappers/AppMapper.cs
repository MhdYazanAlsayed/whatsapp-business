using AutoMapper;
using SprintBuisness.Contracts.Whatsapp.Dtos.MessageResponse;
using SprintBuisness.Contracts.Whatsapp.Dtos;
using SprintBusiness.Domain.Contacts;
using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Messages;
using SprintBusiness.Features.Templates.Commands.SendTemplate.Dtos;
using SprintBusiness.Whatsapp.Dtos.TemplatesDtos.Send;
using SprintBusiness.Shared.Helpers;

namespace SprintBusiness.Api.Mappers
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<SendTemplateComponentDto, SendTemplateMessageComponent>();
            //.ForMember(src => src.Type , dist => dist.MapFrom(x => x.Type.ToString()));

            CreateMap<SendTemplateParameterDto, SendTemplateMessageParametersDto>();
                //.ForMember(src => src.Type, dist => dist.MapFrom(x => x.Type.ToString()));

            CreateMap<SendTemplateCurrencyParameterDto, SendTemplateMessageCurrencyParametersDto>();

            CreateMap<SendTemplateDateTimeParameterDto, SendTemplateMessageDateTimeParametersDto>();

            CreateMap<SendTemplateFileParameterDto, SendTemplateMessageFileDto>()
                .ForMember(src => src.Link , dist => dist.MapFrom(x =>
                ApiEnvironment.Url + "Templates/" + x.FileName));

            CreateMap<Message, SignalRFlowMessageResponse>();
            CreateMap<Conversation, SignalRConversationResponse>();
            CreateMap<Contact, SignalRContactResposne>();

        }
    }
}
