using MediatR;
using Microsoft.AspNetCore.Http;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.Templates.Commands.UploadMedia
{
    public class UploadTemplateMediaCommand : IRequest<ResultDto<UploadTemplateMediaResult>>
    {
        public UploadTemplateMediaCommand(IFormFile file)
        {
            File = file;
        }

        public UploadTemplateMediaCommand(UploadTemplateMediaDto dto)
        {
            File = dto.File;
        }

        public IFormFile File { get; set; }
    }
}
