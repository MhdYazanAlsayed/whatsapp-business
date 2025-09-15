using SprintBuisness.Contracts.Markers;
using SprintBusiness.Domain.Templates;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Whatsapp.Dtos;
using SprintBusiness.Whatsapp.Dtos.TemplatesDtos.Create;
using SprintBusiness.Whatsapp.Dtos.TemplatesDtos.Send;

namespace SprintBuisness.Whatapp
{
    public interface IWhatsappProvider : IScopedDependency
    {
        Task<ResultDto<UploadMediaResponseDto>> UploadMediaAsync(UploadMediaDto dto);
        Task<ResultDto> SendTextMessageAsync(SendMessageDto dto);
        Task<ResultDto> SendReplayButtonMessageAsync(SendInteractiveMessageDto dto);
        Task<ResultDto> SendListMessageAsync(SendMenuMessageDto dto);
        Task<ResultDto> SendTemplateMessageAsync(SendTemplateMessageDto dto);
        Task<ResultDto<List<Template>>> GetAllTemplatesAsync();
        Task<bool> CheckPhoneNumberAsync(string phoneNumber);
        Task<ResultDto<CreateTemplateResponse>> CreateTemplateAsync(CreateTemplateDto dto);
    }
}
